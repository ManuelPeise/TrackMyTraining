using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Shared.Models.Auth;

namespace Web.Core.Components.Pages.Auth
{
    public partial class EmailConfirmationPage
    {
        [Parameter]
        public string Parameter { get; set; }
        private RegistrationResponse RegistrationResponse { get; set; }

        public bool IsLoading { get; set; } = false;

        private void SetIsLoading(bool isLoading)
        {
            IsLoading = isLoading;
        }

        protected override void OnInitialized()
        {   
           RegistrationResponse = JsonConvert.DeserializeObject<RegistrationResponse>(Parameter);
        }

    }
}
