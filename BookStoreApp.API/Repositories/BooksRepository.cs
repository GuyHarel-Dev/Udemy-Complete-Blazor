using AutoMapper;
using BookStoreApp.API.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Repositories
{
    public class BooksRepository : GenericRepository<Book, BookStoreDbContext>, IBooksRepository
    {

        private readonly IMapper mapper;
        public BooksRepository(BookStoreDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            this.mapper = mapper;
        }
    }
}
