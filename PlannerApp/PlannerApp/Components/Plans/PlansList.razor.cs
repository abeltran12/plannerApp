using Microsoft.AspNetCore.Components;
using PlannerApp.Client.Services.Exceptions;
using PlannerApp.Client.Services.Interfaces;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Components
{
    public partial class PlansList
    {
        [Inject]
        public IPlanService PlanService { get; set; }

        private bool _isBussy = false;

        private string _errorMessage = string.Empty;

        private int _pageNumber = 1;

        private int _pageSize = 10;

        private int _totalPages = 1;

        private string _query = string.Empty;

        private List<PlanSumary> _planSumaries = new();

        private async Task<PagedList<PlanSumary>> GetPlansAsync(string query = "", int pageNumber = 1, int pageSize = 10)
        {
            _isBussy = true;

            try
            {
                var result = await PlanService.GetPlansAsync(query, pageNumber, pageSize);
                _planSumaries = result.Value.Records.ToList();
                _pageNumber = result.Value.Page;
                _pageSize = result.Value.PageSize;
                _totalPages = result.Value.TotalPages;
                return result.Value;
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.apiErrorResponse.Message;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }

            _isBussy = false;
            return null;
        }
    }
}