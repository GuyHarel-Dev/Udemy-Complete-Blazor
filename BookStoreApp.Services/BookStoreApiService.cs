using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using BookStoreApp.Services.Interfaces;
using BookStoreApp.Services.Providers;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStoreApp.Services
{
    public class BookStoreApiService : IBookStoreApiService
    {
        private readonly BookStoreApiClientFactory factory;
        private readonly BookStoreAppApiClient apiClient;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private const string TOKEN_NAME = "accessToken";

        public BookStoreApiService(
            BookStoreApiClientFactory factory, 
            ILocalStorageService localStorage,
            AuthenticationStateProvider authenticationStateProvider)
        {
            this.factory = factory;
            this.localStorage = localStorage;
            this.authenticationStateProvider = authenticationStateProvider;
            this.apiClient = factory.CreateApiClient();
        }

        public BookStoreAppApiClient GetApiHttpClient()
        {
            return apiClient;
        }

        public Response<T> ConvertApiException<T>(ApiException exception)
        {
            if (exception != null && exception.StatusCode == 400)
            {
                return new Response<T>()
                {
                    Message = "Validation errors have occured.",
                    ValidationErrors = exception.Response,
                    Success = false
                };
            }
            if (exception != null && exception.StatusCode == 404)
            {
                return new Response<T>()
                {
                    Message = "The requested item could not be found.",
                    Success = false
                };
            }

            return new Response<T>()
            {
                Message = "Something went wrong.",
                Success = false
            };
        }

        public async Task GetBearerToken()
        {
            var token = await localStorage.GetItemAsync<string>(TOKEN_NAME);
            if (token != null)
            {
                apiClient.HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<bool> AuthenticateAsync(LoginUserDto loginUserDto)
        {
            var reponse = await GetApiHttpClient().LoginAsync(loginUserDto);

            // Store token
            await localStorage.SetItemAsync(TOKEN_NAME, reponse.Token);

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
