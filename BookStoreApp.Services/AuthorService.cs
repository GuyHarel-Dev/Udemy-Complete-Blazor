using Blazored.LocalStorage;
using BookStoreApp.Models.Author;
using BookStoreApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Services
{
    public class AuthorService : BookStoreApiService, IAuthorService
    {
        private readonly BookStoreApiClientFactory factory;

        public AuthorService(BookStoreApiClientFactory factory, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider) 
            : base(factory, localStorage, authenticationStateProvider)
        {
            this.factory = factory;
        }

        public async Task<Response<List<Author>>> GetAuthors()
        {
            Response<List<Author>> response;

            try
            {
                var client = factory.CreateApiClient();

                await GetBearerToken();
                var data = await client.AuthorsAllAsync();
                response = new Response<List<Author>>
                {
                    Data = data.ToList(),
                    Success = true
                };
               
            }
            catch (ApiException ex)
            {
                response = ConvertApiException<List<Author>>(ex);
            }

            return response;
        }
    }
}
