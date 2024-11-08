using Microsoft.AspNetCore.Components;
using Shared.Enums;
using Web.Core.Shared.Models.Navigation;
using Web.Core.Shared.Resources;
using Web.Core.Shared.Services.Interfaces;

namespace Web.Core.Components.Layout.Components
{
    public partial class NavList
    {
        [Inject]
        public IResourceService<Administration>? CommonService { get; set; }
        [Parameter] public string? Css { get; set; }
        [Parameter] public bool Expanded { get; set; }
        [Parameter] public UserRoleEnum UserRole { get; set; }

        private List<NavListItemModel>? _administrationNavItems;

        protected override void OnInitialized()
        {
            _administrationNavItems = new List<NavListItemModel>
            {
                new NavListItemModel
                {
                    Id = "api-dashboard",
                    Label = CommonService.GetResource("LabelApiDashboard"),
                    To= "/api-performance-dashboard"
                },

            };
        }
    }
}
