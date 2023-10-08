using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSocialNet.Repository
{
    public interface IGenericRepository<T>
    {
        Task<T> GetByIdAsync (int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<T> GetSingleAsync<T>(string storedProcedureName, object parameters = null);
        Task<IEnumerable<T>> GetListAsync<T>(string storedProcedureName, object parameters = null);
        Task<bool> PerformNonQueryAsync(string storedProcedureName, object parameters = null);
    }
}