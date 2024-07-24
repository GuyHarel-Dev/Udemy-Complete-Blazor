
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreApp.API.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Repositories
{
    public class GenericRepository<T, TDbContext> : IGenericRepository<T>
        where T : class
        where TDbContext : DbContext
    {
        private readonly TDbContext dbContext;
        private readonly IMapper mapper;

        public GenericRepository(TDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<T> AddAsync(T entity)
        {
            await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            dbContext.Set<T>().Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public Task<bool> ExistAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await GetAsync(id);
            return entity != null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return await dbContext.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            dbContext.Update(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<VirtualizeResponse<TResult>> GetAllAsync<TTable, TResult>(QueryParameters queryParameters) 
            where TResult : class
            where TTable : class
        {
           var totalSize = await dbContext.Set<TTable>().CountAsync();
            var items = await dbContext.Set<TTable>()
                .Skip(queryParameters.StartIndex)
                .Take(queryParameters.PageSize)
                .ProjectTo<TResult>(mapper.ConfigurationProvider)
                .ToListAsync();

            return new VirtualizeResponse<TResult> { Items = items, TotalSize = totalSize };    
        }
    }
}
