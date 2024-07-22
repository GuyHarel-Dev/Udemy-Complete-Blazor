using BookStoreApp.Models.Author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<Response<List<AuthorReadDto>>> GetAuthors();
        Task<Response<AuthorReadDto>> GetAuthor(int id);
        Task<Response<int>> CreateAuthor(AuthorCreateDto authorCreateDto);
        Task<Response<int>> EditAuthor(int id, AuthorCreateDto authorCreateDto);
    }
}
