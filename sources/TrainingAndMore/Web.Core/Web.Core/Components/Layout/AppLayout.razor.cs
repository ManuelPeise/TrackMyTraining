using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Web.Core.Providers;
using Shared.Models.Auth;
using Shared.Enums;
using Microsoft.Extensions.Localization;
using Web.Core.Resources;
using Web.Core.Client.Components.Models;

namespace Web.Core.Components.Layout
{
    public partial class AppLayout
    {
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

            _currentUser = ((CustomAuthenticationStateProvider)Provider).CurrentUser;

            _userMenuDropdownItems = new List<DropdownItem>
            {
                new DropdownItem
                {
                    ItemType = DropdownItemTypeEnum.Link,
                    Label =  Localizer[Common.LabelProfile],
                    To = $"/profile/{_currentUser.Id}"
                },
                new DropdownItem
                {
                    ItemType = DropdownItemTypeEnum.Button,
                    Label = Localizer[Common.LabelLogout],
                    Callback = logOutCallback
                }
            };
        }

        private async Task OnLogout()
        {
            await ((CustomAuthenticationStateProvider)Provider).OnLogout();

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
