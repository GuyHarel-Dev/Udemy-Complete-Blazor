using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BookStoreApp.Services.Providers
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorage;
        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;

        private ClaimsPrincipal userNotLoggedIn;

        public ApiAuthenticationStateProvider(ILocalStorageService localStorage, JwtSecurityTokenHandler jwtSecurityTokenHandler)
        {
            this.localStorage = localStorage;
            this.jwtSecurityTokenHandler = jwtSecurityTokenHandler;
            userNotLoggedIn = new ClaimsPrincipal(new ClaimsIdentity()); // identity sans claims dedans, donc représent absence d'utilisateur authentifié
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedToken = await GetSavedToken("accessToken");

            if (savedToken == null)
            {
                var claimsIdentity = new ClaimsIdentity(); 
                return new AuthenticationState(userNotLoggedIn);
            }

            // Vérifier si le token est expiré
            if (savedToken.ValidTo < DateTime.UtcNow)
            {
                return new AuthenticationState(userNotLoggedIn);
            }

            // Retourner les claims du token dans le Principal
            var user = CreateUserClaimPrincipal(savedToken);
            return new AuthenticationState(user);

        }
        
        public async Task LoggedIn()
        {
            var savedToken = await GetSavedToken("accessToken");
            var user = CreateUserClaimPrincipal(savedToken);
            var authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);
        }

        public async Task LoggedOut()
        {
            await localStorage.RemoveItemAsync("accessToken");
            var authState = Task.FromResult(new AuthenticationState(userNotLoggedIn));
            NotifyAuthenticationStateChanged(authState);
        }

        private async Task<JwtSecurityToken?> GetSavedToken(string tokenName)
        {
            // Obtenir le token du fureteur
            var savedToken = await localStorage.GetItemAsync<string>(tokenName);

            if (savedToken == null)
                return null;

            // Parser le token
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(savedToken);

            return tokenContent;
        }

        private ClaimsPrincipal CreateUserClaimPrincipal(JwtSecurityToken jwtSecurityToken)
        {
            var claims = jwtSecurityToken.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, jwtSecurityToken.Subject));
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")); // le "jwt" indique que les claims proviennent de l'auth Jwt
            return user;
        }

    }
}
