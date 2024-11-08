using Microsoft.AspNetCore.Components;
using Shared.Models.Administration;
using Web.Core.Shared.Models.Components;

namespace Web.Core.Shared.ViewModels.Interfaces
{
    public interface IApiPerformanceViewModel : IViewModelBase
    {
        public int ApiStatisticId { get; }
        public List<ApiPerformanceStatistics> ApiPerformanceStatistics { get; }
        List<SelectItem> ControllerDropdownItems { get; }
        public SelectItem SelectedController { get; }
        Task InitializeAsync();
        void HandleSelectedItemChanged(ChangeEventArgs e);
    }
}
