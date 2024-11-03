using Microsoft.AspNetCore.Components;
using Web.Core.Shared;
using Web.Core.Shared.Models;

namespace Web.Core.Client.Components.Dropdowns
{
    public partial class Dropdown
    {
        private bool _isExpanded = false;

        [Parameter]
        public string? Label { get; set; }
        [Parameter]
        public string? LabelStyle { get; set; }
        [Parameter]
        public string? IconStyle { get; set; }
        [Parameter]
        public List<DropdownItem>? Items { get; set; }
        [Parameter]
        public string? ButtonStyle { get; set; }

        [Parameter]
        public string? MenuStyle { get; set; }
        private void Show()
        {
            _isExpanded = true; 
        }
        private void Hide()
        {
            _isExpanded = false;
        }

        private async Task InvokeCallback(Delegates.AsyncCallback? callback)
        {
            if (callback != null)
            {
                await callback();
                Hide();
            }
           
        }
    }
}
