using BookStoreApp.API.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Repositories
{
    public class BooksRepository : GenericRepository<Book, DbContext>, IBooksRepository
    {
        public BooksRepository(DbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
