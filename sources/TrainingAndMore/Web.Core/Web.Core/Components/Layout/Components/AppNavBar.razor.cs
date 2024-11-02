using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Web.Core.Providers;
using Shared.Models.Auth;

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
                var provider = (CustomAuthenticationStateProvider)Provider;

                await provider.OnLogout();
            }
        }
    }
}
