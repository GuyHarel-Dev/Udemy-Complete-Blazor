namespace BookStoreApp.API.Repositories
{
    public interface IGenericRepository<T>
        where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<VirtualizeResponse<TResult>> GetAllAsync<TTable, TResult>(QueryParameters queryParameters)
            where TResult : class
            where TTable : class;
        Task<T> GetAsync(int? id);
        Task<T> AddAsync(T entity);
        Task DeleteAsync(int id);
        Task UpdateAsync(T entity);

        Task<bool> ExistAsync(int id);
    }
}
