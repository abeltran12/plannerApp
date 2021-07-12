using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Services.Exceptions;
using PlannerApp.Client.Services.Interfaces;
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
    public partial class RegisterForm : ComponentBase
    {
        //[Inject]
        //public IAuthenticationService AuthenticationService { get; set; }

        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private RegisterRequest _register = new RegisterRequest();

        private bool _isBussy { get; set; }

        private string _errorMessage = string.Empty;

        private async Task RegisterUserAsync()
        {
            _isBussy = true;
            _errorMessage = string.Empty;

            //try
            //{
            //    await AuthenticationService.RegisterUserAsync(_register);
            //    NavigationManager.NavigateTo("/authentication/login");
            //}
            //catch(ApiException ex)
            //{
            //    _errorMessage = ex.apiErrorResponse.Message;
            //}
            //catch(Exception ex)
            //{
            //    _errorMessage = ex.Message;
            //}

            var response = await HttpClient.PostAsJsonAsync("/api/v2/auth/register", _register);

            if (response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/");
            }
            else
            {
                var errorResult = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                _errorMessage = errorResult.Message;
            }

            _isBussy = false;
        }

        private void RedirectToLogin()
        {
            NavigationManager.NavigateTo("/authentication/login");
        }
    }
}
