
using Microsoft.AspNetCore.Components;
using Web.Core.Shared.ViewModels.Interfaces;

namespace Web.Core.Components.Pages.Administration
{
    public partial class ApiPerformanceDashboard
    {
        [Inject]
        public IApiPerformanceViewModel ViewModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await ViewModel.InitializeAsync();

        }
    }
}
