using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlannerApp
{
    public class JwtAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;

        public JwtAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (await _localStorage.ContainKeyAsync("access_token"))
            {
                //el usuario está logueado
                var tokenAsString = await _localStorage.GetItemAsStringAsync("access_token");
                var tokenHanlder = new JwtSecurityTokenHandler();

                var token = tokenHanlder.ReadJwtToken(tokenAsString);
                var identity = new ClaimsIdentity(token.Claims, "Bearer");
                var user = new ClaimsPrincipal(identity);
                var authState = new AuthenticationState(user);

                //con esto se notifica a todos los demas componentes que se ingreso
                NotifyAuthenticationStateChanged(Task.FromResult(authState));

                return authState;
            }

            //quiere decir que el usuario no esta registrado
            return new AuthenticationState(new ClaimsPrincipal());
        }
    }
}
