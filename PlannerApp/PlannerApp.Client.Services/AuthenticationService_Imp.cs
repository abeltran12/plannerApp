using PlannerApp.Client.Services.Exceptions;
using PlannerApp.Client.Services.Interfaces;
using PlannerApp.Shared.Models;
using PlannerApp.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PlannerApp.Client.Services
{
    public class AuthenticationService_Imp : IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService_Imp(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponses> RegisterUserAsync(RegisterRequest registerRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/v2/Auth/Register", registerRequest);
            
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponses>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }
    }
}
