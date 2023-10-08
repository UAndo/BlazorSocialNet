using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace BlazorSocialNet.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IDbConnection _connection;
        private readonly IConfiguration _configuration;

        public GenericRepository(IConfiguration configuration)
        {
            _configuration = configuration;      
            _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<bool> AddAsync(T entity)
        {
            int rowAffected = 0;
            try
            {
                string tableName = GetTableName();
                string columns = GetColumns(excludeKey: true);
                string properties = GetPropertyNames(excludeKey: true);
                string query = "INSERT INTO " + tableName + " (" + columns + ") VALUES (" + properties + ")";

                rowAffected = await _connection.ExecuteAsync(query, entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return rowAffected > 0;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            int rowAffected = 0;
            try
            {
                string tableName = GetTableName();
                string keyColumn = GetKeyColumnName();
                string keyProperty = GetKeyPropertyName();
                string query = "DELETE FROM " + tableName + " WHERE " + keyColumn + " = " + keyProperty;

                rowAffected = await _connection.ExecuteAsync(query, entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return rowAffected > 0;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IEnumerable<T> result = null;
            try
            {
                string tableName = GetTableName();
                string query = "SELECT * FROM " + tableName;

                result = await _connection.QueryAsync<T>(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            IEnumerable<T> result = null;
            try
            {
                string tableName = GetTableName();
                string keyColumn = GetKeyColumnName();
                string query = "SELECT * FROM " + tableName + " WHERE " + keyColumn + " = " + id;

                result = await _connection.QueryAsync<T>(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result.FirstOrDefault();
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            int rowsEffected = 0;
            try
            {
                string tableName = GetTableName();
                string keyColumn = GetKeyColumnName();
                string keyProperty = GetKeyPropertyName();

                StringBuilder query = new();
                query.Append($"UPDATE {tableName} SET ");

                foreach (var property in GetProperties(true))
                {
                    var columnAttr = property.GetCustomAttribute<ColumnAttribute>();

                    string propertyName = property.Name;
                    string columnName = columnAttr.Name;

                    query.Append($"{columnName} = @{propertyName},");
                }

                query.Remove(query.Length - 1, 1);

                query.Append($" WHERE {keyColumn} = @{keyProperty}");

                rowsEffected = await _connection.ExecuteAsync(query.ToString(), entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return rowsEffected > 0;
        }

        public async Task<T> GetSingleAsync<T>(string storedProcedureName, object parameters = null)
        {
            var result = await _connection.QuerySingleOrDefaultAsync<T>(
                storedProcedureName,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<IEnumerable<T>> GetListAsync<T>(string storedProcedureName, object parameters = null)
        {
           var result = await _connection.QueryAsync<T>(
                storedProcedureName,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<bool> PerformNonQueryAsync(string storedProcedureName, object parameters = null)
        {
            var result = await _connection.ExecuteAsync(
                storedProcedureName,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result > 0;
        }

        private static string GetTableName()
        {
            var type = typeof(T);
            var tableAttr = type.GetCustomAttribute<TableAttribute>();
            if (tableAttr != null)
            {
                var tableName = tableAttr.Name;
                return tableName;
            }

            return type.Name + "s";
        }

        public static string GetKeyColumnName()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object[] KeyAttributes = property.GetCustomAttributes(typeof(KeyAttribute), true);

                if (KeyAttributes != null && KeyAttributes.Length > 0)
                {
                    object[] columnAttributes = property.GetCustomAttributes(typeof(ColumnAttribute), true);

                    if (columnAttributes != null && columnAttributes.Length > 0)
                    {
                        ColumnAttribute columnAttribute = (ColumnAttribute)columnAttributes[0];
                        return columnAttribute.Name;
                    }
                }
                else 
                {
                    return property.Name;
                }
            }
            return null;
        }

        private static string GetColumns(bool excludeKey = false)
        {
            var type = typeof(T);
            var columns = string.Join(", ", type.GetProperties()
                .Where(p => !excludeKey || !p.IsDefined(typeof(KeyAttribute)))
                .Select(p =>
                {
                    var columnAttr = p.GetCustomAttribute<ColumnAttribute>();
                    return columnAttr != null ? columnAttr.Name : p.Name;
                }));

            return columns;
        }

        protected string GetPropertyNames(bool excludeKey = false)
        {
            var properties = typeof(T).GetProperties()
                .Where(p => !excludeKey || !p.IsDefined(typeof(KeyAttribute)));
            
            var values = string.Join(", ", properties.Select(p => "@" + p.Name));

            return values;
        }

        protected IEnumerable<PropertyInfo> GetProperties(bool excludeKey = false)
        {
            var properties = typeof(T).GetProperties()
                .Where(p => !excludeKey || !p.IsDefined(typeof(KeyAttribute)));

            return properties;            
        }

        protected string GetKeyPropertyName()
        {
            var properties = typeof(T).GetProperties()
                .Where(p => p.GetCustomAttribute<KeyAttribute>() != null);
            
            if (properties.Any()) 
            {
                return properties.FirstOrDefault().Name;
            }

            return null;
        }
    }
}