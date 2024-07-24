using AutoMapper;
using BookStoreApp.API.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Repositories
{
    public class AuthorsRepository : GenericRepository<Author, BookStoreDbContext>, IAuthorsRepository
    {
        private readonly IMapper mapper;

        public AuthorsRepository(BookStoreDbContext dbContext, IMapper mapper) :  base(dbContext, mapper)
        {
            this.mapper = mapper;
        }

        public async Task CreerDonneesVirtualisation()
        {
            for (int i = 0; i < 1000; i++)
            {
                var author = new Author
                {
                    FirstName = $"Prenom_{i.ToString("D6")}",
                    LastName = $"Nom_{i.ToString("D6")}",
                    Bio = $"Bio_{i.ToString("D6")}"
                };
                await this.AddAsync(author);
            }
        }
    }
}
