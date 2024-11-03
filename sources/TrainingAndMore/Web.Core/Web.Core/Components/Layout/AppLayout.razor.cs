using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Shared.Models.Auth;
using Microsoft.Extensions.Localization;
using Web.Core.Shared;
using Web.Core.Shared.Resources;
using Web.Core.Shared.Services.Interfaces;
using Web.Core.Shared.Models;
using Web.Core.Shared.Enums;
using Web.Core.Shared.Providers;

namespace Web.Core.Components.Layout
{
    public partial class AppLayout
    {
        [Inject]
        public IResourceService<Common>? ResourceService { get; set; }
        [Inject]
        [CascadingParameter]
        public AuthenticationStateProvider? Provider { get; set; }
        [Inject]
        public IStringLocalizer<Common>? Localizer { get; set; }

        private bool _isExpanded = false;
        private UserExportModel? _currentUser;
        private List<DropdownItem>? _userMenuDropdownItems;

        protected override void OnInitialized()
        {
            var logOutCallback = new Delegates.AsyncCallback(OnLogout);

            _currentUser = ((CustomAuthStateProvider)Provider).CurrentUser;

            _userMenuDropdownItems = new List<DropdownItem>
            {
                new DropdownItem
                {
                    ItemType = DropdownItemTypeEnum.Link,
                    Label =  ResourceService.GetResource("LabelProfile"),
                    To = $"/profile/{_currentUser.Id}"
                },
                new DropdownItem
                {
                    ItemType = DropdownItemTypeEnum.Button,
                    Label = ResourceService.GetResource("LabelLogout"),
                    Callback = logOutCallback
                }
            };
        }

        private async Task OnLogout()
        {
            await ((CustomAuthStateProvider)Provider).OnLogout();

        }

        private bool isActive = false;

        private void Show()
        {
            isActive = true;
        }

        private void Hide()
        {
            isActive = false;
        }

        private async Task LogOutAsync()
        {

        }

        private void ToggleExpanded()
        {
            _isExpanded = !_isExpanded;
        }


    }
}
