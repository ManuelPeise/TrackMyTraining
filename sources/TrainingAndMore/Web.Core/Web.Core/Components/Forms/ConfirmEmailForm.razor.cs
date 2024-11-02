using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Shared.Models.Auth;
using Web.Core.Models;
using Web.Core.Providers;

namespace Web.Core.Components.Forms
{
    public partial class ConfirmEmailForm
    {

        [Inject]
        public NavigationManager? NavigationManager { get; set; }
        [Parameter]
        public RegistrationResponse? RegistrationResponse { get; set; }
        [Inject]
        [CascadingParameter]
        public AuthenticationStateProvider Provider { get; set; }

        [Parameter]
        public EventCallback<bool> SetIsLoading { get; set; }

        public AccountActivationRequestModel? RequestModel { get; set; }

        private bool Failed = false;
        protected override void OnInitialized()
        {
            RequestModel = new AccountActivationRequestModel
            {
                UserId = RegistrationResponse.UserId
            };
        }

        public async Task OnActivate()
        {
            await SetIsLoading.InvokeAsync(true);

            var result = await ((CustomAuthenticationStateProvider)Provider).OnActivateAccount(RequestModel);

            await SetIsLoading.InvokeAsync(false);
            if (result != null)
            {
                NavigationManager.NavigateTo($"/login/{result.Email}");
            }

            Failed = true;
        }
    }
}
