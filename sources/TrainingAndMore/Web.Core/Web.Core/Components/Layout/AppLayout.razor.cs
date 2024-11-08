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
using Web.Core.Shared.Models.Navigation;
using Shared.Enums;

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

        protected override void OnInitialized()
        {
            var logOutCallback = new Delegates.AsyncCallback(OnLogout);
            _currentUser = ((CustomAuthStateProvider)Provider).CurrentUser;
            _userRole = (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), _currentUser.UserRole);
        }

        private async Task OnLogout()
        {
            await ((CustomAuthStateProvider)Provider).OnLogout();

        }

        private bool isActive = false;

        private void Show()
        {
            _isExpanded = true;
        }

        private void Hide()
        {
            _isExpanded = false;
        }

    }
}
