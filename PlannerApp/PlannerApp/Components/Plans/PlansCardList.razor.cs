using Microsoft.AspNetCore.Components;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PlannerApp.Components
{
    public partial class PlansCardList
    {
        private bool _IsBussy { get; set; }

        private int _pageSize = 10;

        private int _pageNumber = 1;

        private string _query = string.Empty;

        private PagedList<PlanSumary> _result = new();

        [Parameter]
        public Func<string,int,int,Task<PagedList<PlanSumary>>> FetchPlans { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _IsBussy = true;
            _result = await FetchPlans?.Invoke(_query, _pageNumber, _pageSize);
            _IsBussy = false;
     
        }
    }
}