using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Web.Core.Providers;
using Web.Core.Shared.Services.Interfaces;
using Web.Core.Shared.Resources;
using Web.Core.Shared.Models.Auth;

namespace Web.Core.Components.Forms
{
    public partial class LoginForm
    {

        [Inject]
        public IResourceService<Common>? ResourceService { get; set; }

        [Inject]
        public NavigationManager? NavigationManager { get; set; }
        [Inject]
        [CascadingParameter]
        public AuthenticationStateProvider? Provider { get; set; }
        [Parameter]
        public bool ShowRegisterLink { get; set; }
        [Parameter]
        public string? Email { get; set; }
        public LoginRequest? LoginRequest { get; set; }
        [Parameter] 
        public EventCallback<bool> SetIsLoading { get; set; }

        protected override void OnInitialized()
        {
            LoginRequest = new LoginRequest
            {
                Email = Email ?? string.Empty,
                Password = string.Empty,
            };
        }

        public async Task OnLogin()
        {
            await SetIsLoading.InvokeAsync(true);

            await ((CustomAuthenticationStateProvider)Provider).OnLogin(LoginRequest);

            await SetIsLoading.InvokeAsync(true);

            NavigationManager.NavigateTo("/counter");
        }
    }
}
