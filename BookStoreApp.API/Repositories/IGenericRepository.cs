namespace BookStoreApp.API.Repositories
{
    public interface IGenericRepository<T>
        where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(int? id);
        Task<T> AddAsync(T entity);
        Task DeleteAsync(int id);
        Task UpdateAsync(T entity);

        Task<bool> ExistAsync(int id);
    }
}
