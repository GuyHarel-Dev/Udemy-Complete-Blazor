using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Services
{
    public class BookStoreApiClientFactory
    {
        private readonly IHttpClientFactory factory;
        public BookStoreApiClientFactory(IHttpClientFactory factory)
        {
              this.factory = factory;
        }

        public BookStoreAppApiClient CreateApiClient()
        {
            var client = factory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7094");
            return new BookStoreAppApiClient(client);
        }
    }
}
