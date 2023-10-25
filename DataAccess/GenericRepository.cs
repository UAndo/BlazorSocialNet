using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using Dapper;
using Microsoft.Extensions.Configuration;
using static Dapper.SqlMapper;

namespace BlazorSocialNet.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly IDbConnection _connection;
        private readonly IConfiguration _configuration;

        public GenericRepository(IConfiguration configuration)
        {
            _configuration = configuration;      
            _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<bool> AddAsync(TEntity entity)
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

        public async Task<bool> DeleteAsync(TEntity entity)
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

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            IEnumerable<TEntity> result = null;
            try
            {
                string tableName = GetTableName();
                string query = "SELECT * FROM " + tableName;

                result = await _connection.QueryAsync<TEntity>(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            IEnumerable<TEntity> result = null;
            try
            {
                string tableName = GetTableName();
                string keyColumn = GetKeyColumnName();
                string query = "SELECT * FROM " + tableName + " WHERE " + keyColumn + " = " + id;

                result = await _connection.QueryAsync<TEntity>(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result.FirstOrDefault();
        }

        public async Task<bool> UpdateAsync(TEntity entity)
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

        public async Task<TModel> GetSingleAsync<TModel>(string storedProcedureName, object? parameters = null) where TModel : class
        {
            var result = await _connection.QuerySingleOrDefaultAsync<TModel>(
                storedProcedureName,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<IEnumerable<TModel>> GetListAsync<TModel>(string storedProcedureName, object? parameters = null) where TModel : class
        {
            var result = await _connection.QueryAsync<TModel>(
                storedProcedureName,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<TEntity> GetSingleAsync(string storedProcedureName, object? parameters = null)
        {
            return await GetSingleAsync<TEntity>(storedProcedureName, parameters);
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(string storedProcedureName, object? parameters = null)
        {
            return await GetListAsync<TEntity>(storedProcedureName, parameters);
        }

        public async Task<bool> PerformNonQueryAsync(string storedProcedureName, object? parameters = null)
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
            var type = typeof(TEntity);
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
            PropertyInfo[] properties = typeof(TEntity).GetProperties();

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
            return string.Empty;
        }

        private static string GetColumns(bool excludeKey = false)
        {
            var type = typeof(TEntity);
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
            var properties = typeof(TEntity).GetProperties()
                .Where(p => !excludeKey || !p.IsDefined(typeof(KeyAttribute)));
            
            var values = string.Join(", ", properties.Select(p => "@" + p.Name));

            return values;
        }

        protected IEnumerable<PropertyInfo> GetProperties(bool excludeKey = false)
        {
            var properties = typeof(TEntity).GetProperties()
                .Where(p => !excludeKey || !p.IsDefined(typeof(KeyAttribute)));

            return properties;            
        }

        protected string GetKeyPropertyName()
        {
            var properties = typeof(TEntity).GetProperties()
                .Where(p => p.GetCustomAttribute<KeyAttribute>() != null);
            
            if (properties.Any()) 
            {
                return properties.FirstOrDefault().Name;
            }

            return string.Empty;
        }
    }
}