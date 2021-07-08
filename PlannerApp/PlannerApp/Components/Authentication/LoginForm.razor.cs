using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PlannerApp.Shared.Models;
using PlannerApp.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PlannerApp.Components
{
    public partial class LoginForm : ComponentBase
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }

        private Login _login = new Login();

        private bool _isBussy { get; set; }

        private string _errorMessage = string.Empty;

        private async Task LoginUserAsync()
        {
            _isBussy = true;
            _errorMessage = string.Empty;

            var response = await HttpClient.PostAsJsonAsync("/api/v2/auth/Login", _login);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponses<LoginResult>>();
                //guardar el token
                await LocalStorageService.SetItemAsStringAsync("access_token", result.Value.Token);
                await LocalStorageService.SetItemAsync<DateTime>("expiry_date", result.Value.ExpiryDate);

                //el que actualiza el token
                await AuthenticationStateProvider.GetAuthenticationStateAsync();
                NavigationManager.NavigateTo("/");
            }
            else
            {
                var errorResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                _errorMessage = errorResult.Message;
            }

            _isBussy = false;
        }
    }
}
