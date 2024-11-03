using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Web.Core.Shared.Providers;

namespace Web.Core.Components.Layout.Components
{
    public partial class AppNavBar
    {
        [Inject]
        [CascadingParameter]
        private AuthenticationStateProvider? Provider { get; set; }

        public async Task OnLogout()
        {
            if (Provider != null)
            {
                var provider = (CustomAuthStateProvider)Provider;

                await provider.OnLogout();
            }
        }
    }
}
