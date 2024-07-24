using BookStoreApp.API.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Repositories
{
    public class AuthorsRepository : GenericRepository<Author, DbContext>, IAuthorsRepository
    {
        public AuthorsRepository(DbContext dbContext) :  base(dbContext)
        {

        }

    }
}
