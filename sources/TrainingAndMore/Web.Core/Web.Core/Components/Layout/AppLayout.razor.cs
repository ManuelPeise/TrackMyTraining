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
using Shared.Enums;
using Web.Core.Shared.Models.Navigation;

namespace Web.Core.Components.Layout
{
    public partial class AppLayout
    {
        [Inject]
        public IResourceService<Common>? CommonService { get; set; }
        [Inject]
        public IResourceService<Administration>? AdministrationService { get; set; }
        [Inject]
        [CascadingParameter]
        public AuthenticationStateProvider? Provider { get; set; }
        [Inject]
        public IStringLocalizer<Common>? Localizer { get; set; }

        private bool _isExpanded = false;
        private UserExportModel? _currentUser;
        private List<DropdownItem>? _userMenuDropdownItems;
        private UserRoleEnum _userRole;

        private NavItemModel _administrationNavItemModel;

        protected override void OnInitialized()
        {
            var logOutCallback = new Delegates.AsyncCallback(OnLogout);
            _currentUser = ((CustomAuthStateProvider)Provider).CurrentUser;
            _userRole = (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), _currentUser.UserRole);
            _userMenuDropdownItems = new List<DropdownItem>
            {
                new DropdownItem
                {
                    ItemType = DropdownItemTypeEnum.Link,
                    Label =  CommonService.GetResource("LabelProfile"),
                    To = $"/profile/{_currentUser.Id}"
                },
                new DropdownItem
                {
                    ItemType = DropdownItemTypeEnum.Button,
                    Label = CommonService.GetResource("LabelLogout"),
                    Callback = logOutCallback
                }
            };

            _administrationNavItemModel = new NavItemModel
            {
                Id = "administration",
                ControlId= "administration-item",
                GroupName = AdministrationService.GetResource("LabelAdministration"),
                Icon = "bi bi-wrench-adjustable-circle fs-4 text-light",
                Items = new List<NavigationItem>
                {
                    new NavigationItem
                    {
                        Label = AdministrationService.GetResource("LabelUserAdministration"),
                        To = "/user-administration"
                    }
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
