using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Web.Core.Providers;
using Newtonsoft.Json;
using Web.Core.Shared.Resources;
using Web.Core.Shared.Services.Interfaces;
using Web.Core.Shared.Models.Auth;

namespace Web.Core.Components.Forms
{
    public partial class RegisterForm
    {
        [Inject]
        public IResourceService<Common>? ResourceService { get; set; }
        [Inject]
        public NavigationManager? NavigationManager { get; set; }
        [Inject]
        [CascadingParameter]
        public AuthenticationStateProvider? Provider { get; set; }
        public UserRegistrationRequest RegisterRequest { get; set; } = new();
        [Parameter]
        public EventCallback<bool> SetIsLoading { get; set; }

        private async Task OnRegister()
        {
            await SetIsLoading.InvokeAsync(true);

            var result = await ((CustomAuthenticationStateProvider)Provider).OnRegisterUser(RegisterRequest);

            await SetIsLoading.InvokeAsync(false);

            var json = JsonConvert.SerializeObject(result);

            if (result != null && result.Success)
            {
                NavigationManager.NavigateTo($"/confirm-email/{json}");

            }
        }
    }
}
