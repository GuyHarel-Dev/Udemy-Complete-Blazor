using BookStoreApp.API.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Repositories
{
    public class BooksRepository : GenericRepository<Book, BookStoreDbContext>, IBooksRepository
    {
        public BooksRepository(BookStoreDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
