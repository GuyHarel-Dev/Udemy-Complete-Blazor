using BookStoreApp.API.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Repositories
{
    public class AuthorsRepository : GenericRepository<Author, BookStoreDbContext>, IAuthorsRepository
    {
        public AuthorsRepository(BookStoreDbContext dbContext) :  base(dbContext)
        {

        }

    }
}
