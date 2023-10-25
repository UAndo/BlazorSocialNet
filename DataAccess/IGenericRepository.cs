namespace BlazorSocialNet.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync (Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<bool> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        Task<TEntity> GetSingleAsync(string storedProcedureName, object? parameters = null);
        Task<IEnumerable<TEntity>> GetListAsync(string storedProcedureName, object? parameters = null);
        Task<TModel> GetSingleAsync<TModel>(string storedProcedureName, object? parameters = null) where TModel : class;
        Task<IEnumerable<TModel>> GetListAsync<TModel>(string storedProcedureName, object? parameters = null) where TModel : class;
        Task<bool> PerformNonQueryAsync(string storedProcedureName, object? parameters = null);
    }
}   