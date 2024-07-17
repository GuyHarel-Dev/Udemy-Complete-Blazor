using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Services.Interfaces
{
    public interface IBookStoreApiService
    {
        BookStoreAppApiClient GetApiHttpClient();

        Task<bool> AuthenticateAsync(LoginUserDto loginUserDto);
        Task Logout();
    }
}
