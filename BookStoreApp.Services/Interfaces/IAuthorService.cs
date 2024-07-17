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
        Task<Response<List<Author>>> GetAuthors();
    }
}
