using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using BookStoreApp.Services.Interfaces;
using BookStoreApp.Services.Providers;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStoreApp.Services
{
    public class BookStoreAuthService : IBookStoreAuthService
    {
        private readonly BookStoreApiClientFactory factory;
        private readonly BookStoreAppApiClient apiClient;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public BookStoreAuthService(
            BookStoreApiClientFactory factory, 
            ILocalStorageService localStorage,
            AuthenticationStateProvider authenticationStateProvider)
        {
            this.factory = factory;
            this.apiClient = factory.CreateApiClient();
            this.localStorage = localStorage;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> AuthenticateAsync(LoginUserDto loginUserDto)
        {
            var reponse = await apiClient.LoginAsync(loginUserDto);

            // Store token
            await localStorage.SetItemAsync("accessToken", reponse.Token);

            // Change auth state of the app (to know that an authenticated user is present)
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedIn();

            return true;

        }

        public async Task Logout()
        {
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedOut();
        }
    }
}
