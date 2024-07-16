using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using BookStoreApp.Services.Interfaces;

namespace BookStoreApp.Services
{
    public class BookStoreAuthService : IBookStoreAuthService
    {
        private readonly BookStoreApiClientFactory factory;
        private readonly BookStoreAppApiClient apiClient;
        private readonly ILocalStorageService localStorage;

        public BookStoreAuthService(BookStoreApiClientFactory factory, ILocalStorageService localStorage)
        {
            this.factory = factory;
            this.apiClient = factory.CreateApiClient();
            this.localStorage = localStorage;
        }

        public async Task<bool> AuthenticateAsync(LoginUserDto loginUserDto)
        {
            var reponse = await apiClient.LoginAsync(loginUserDto);

            // Store token

            // Change auth state of the app (to know that an authenticated user is present)


            return true;

        }
    }
}
