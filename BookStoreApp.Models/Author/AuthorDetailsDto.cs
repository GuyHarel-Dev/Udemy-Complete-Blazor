using BookStoreApp.Models.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Models.Author
{
    public class AuthorDetailsDto : AuthorReadDto
    {
        public List<BookReadOnlyDto> Books { get; set; }
    }
}
