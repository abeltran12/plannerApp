using Microsoft.AspNetCore.Components;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerApp.Components
{
    public partial class PlanCard
    {
        [Parameter]
        public PlanSumary planSumary { get; set; }

        [Parameter]
        public bool IsBusy { get; set; }

        [Parameter]
        public EventCallback<PlanSumary> OnViewClicked { get; set; }

        [Parameter]
        public EventCallback<PlanSumary> OnDeleteClicked { get; set; }

        [Parameter]
        public EventCallback<PlanSumary> OnEditClicked { get; set; }
    }
}
