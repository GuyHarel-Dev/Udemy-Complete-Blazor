using Blazored.LocalStorage;
using BookStoreApp.Models;
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

        public async Task<Response<int>> CreateAuthor(AuthorCreateDto authorCreateDto)
        {
            Response<int> response = new Response<int> {  Success = true };

            try
            {
                var client = GetApiHttpClient();
                await GetBearerToken();
                await client.AuthorsPOSTAsync(authorCreateDto);
                
            }
            catch (ApiException ex)
            {
                response = ConvertApiException<int>(ex);
            }

            return response;
        }

        public async Task<Response<int>> DeleteAuthor(int id)
        {
            Response<int> response = new Response<int> { Success = true };

            try
            {
                var client = GetApiHttpClient();
                await GetBearerToken();
                await client.AuthorsDELETEAsync(id);

            }
            catch (ApiException ex)
            {
                response = ConvertApiException<int>(ex);
            }

            return response;
        }

        public async Task<Response<int>> EditAuthor(int id, AuthorCreateDto authorCreateDto)
        {
            Response<int> response = new();

            try
            {
                var client = GetApiHttpClient();
                await GetBearerToken();
                await client.AuthorsPUTAsync(id, authorCreateDto);

            }
            catch (ApiException ex)
            {
                response = ConvertApiException<int>(ex);
            }

            return response;
        }

        public async Task<Response<AuthorReadDto>> GetAuthor(int id)
        {
            Response<AuthorReadDto> response;

            try
            {
                var client = GetApiHttpClient();

                await GetBearerToken();
                var data = await client.AuthorsGETAsync(id);
                response = new Response<AuthorReadDto>
                {
                    Data = new AuthorReadDto { Id = data.Id, FirstName = data.FirstName, LastName= data.LastName, Bio = data.Bio },
                    Success = true
                };

            }
            catch (ApiException ex)
            {
                response = ConvertApiException<AuthorReadDto>(ex);
            }

            return response;
        }

        public async Task<Response<List<AuthorReadDto>>> GetAuthors()
        {
            Response<List<AuthorReadDto>> response;

            try
            {
                var client = GetApiHttpClient();

                await GetBearerToken();
                var data = await client.AuthorsAllAsync();
                response = new Response<List<AuthorReadDto>>
                {
                    Data = data.Select( a => new AuthorReadDto { Id= a.Id, FirstName = a.FirstName, LastName = a.LastName, Bio = a.Bio}).ToList(),
                    Success = true
                };
               
            }
            catch (ApiException ex)
            {
                response = ConvertApiException<List<AuthorReadDto>>(ex);
            }

            return response;
        }

        public async Task<Response<VirtualizeResponse<AuthorReadDto>>> GetAuthorsPage(QueryParameters queryParameters)
        {
            Response<VirtualizeResponse<AuthorReadDto>> response;

            try
            {
                var client = GetApiHttpClient();

                await GetBearerToken();
                var data = await client.PageAsync(queryParameters.StartIndex, queryParameters.PageSize);
                response = new Response<VirtualizeResponse<AuthorReadDto>>();
                response.Data = data;
                response.Success = true;

            }
            catch (ApiException ex)
            {
                response = ConvertApiException<VirtualizeResponse<AuthorReadDto>>(ex);
            }

            return response;
        }
    }
}
