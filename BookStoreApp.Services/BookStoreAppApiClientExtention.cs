using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Services
{
    //public partial interface IBookStoreAppApiClient
    //{
    //}

    public partial class BookStoreAppApiClient
    {
        public HttpClient HttpClient => _httpClient;
    }
}
