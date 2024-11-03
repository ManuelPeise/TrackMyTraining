using BusinessLogic.Shared.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Shared.Models.Api;
using Shared.Models.Auth;
using System.Security.Claims;
using Web.Core.Shared.Models.Auth;

namespace Web.Core.Shared.Providers
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private const string JWT = "jwt-token";

        private readonly IHttpService? _httpService;
        private readonly IJSRuntime _jsRuntime;
        public UserExportModel? CurrentUser { get; private set; }

        public CustomAuthStateProvider(IHttpService httpService, IJSRuntime jsRuntime)
        {
            _httpService = httpService;
            _jsRuntime = jsRuntime;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await GetTokenFromStorage();

            var identity = string.IsNullOrWhiteSpace(token)
                ? new ClaimsIdentity()
                : new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");

            var userDataClaim = identity.FindFirst(BusinessLogic.Shared.ClaimTypes.UserDataClaim)?.Value;

            if (userDataClaim != null)
            {
                CurrentUser = JsonConvert.DeserializeObject<UserExportModel>(userDataClaim);
            }

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task OnLogin(Shared.Models.Auth.LoginRequest loginRequest)
        {
            if ((_httpService != null))
            {
                var response = await _httpService.SendPostRequest<ApiResponse<string>>("Authentication/UserLogin", JsonConvert.SerializeObject(loginRequest));

                if (response != null && response.Success && !string.IsNullOrWhiteSpace(response.Data))
                {
                    await SetTokenAsync(response.Data);
                }
            }
        }

        public async Task OnLogout()
        {
            await _jsRuntime.InvokeAsync<string>("localStorage.removeItem", JWT);

            if (_httpService != null)
            {
                await _httpService.SendGetRequest<ApiResponse<string>>($"Authentication/UserLogout?userId={CurrentUser.Id}");
            }

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task<RegistrationResponse?> OnRegisterUser(UserRegistrationRequest registerRequest)
        {
            if ((_httpService != null))
            {
                var response = await _httpService.SendPostRequest<ApiResponse<RegistrationResponse>>("Authentication/UserRegistration", JsonConvert.SerializeObject(registerRequest));

                if (response != null && response.Success)
                {
                    return response.Data;
                }
            }

            return null;
        }

        public async Task<UserAccountActivationResponse?> OnActivateAccount(AccountActivationRequestModel requestModel)
        {
            if ((_httpService != null))
            {
                var response = await _httpService.SendPostRequest<ApiResponse<UserAccountActivationResponse>>("Authentication/ConfirmEmail", JsonConvert.SerializeObject(requestModel));

                if (response != null && response.Success)
                {
                    return response.Data;
                }
            }

            return null;
        }

        private async Task<string> GetTokenFromStorage() =>
            await _jsRuntime.InvokeAsync<string>("localStorage.getItem", JWT);

        private async Task SetTokenAsync(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                await _jsRuntime.InvokeAsync<string>("localStorage.removeItem", JWT);
            }
            else
            {
                await _jsRuntime.InvokeAsync<string>("localStorage.setItem", JWT, token);
            }

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];


            var jsonBytes = ParseBase64(payload);
            var keyValuePairs = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            return keyValuePairs.Select(x => new Claim(x.Key, x.Value.ToString()));

        }

        private static byte[] ParseBase64(string input)
        {
            switch (input.Length % 4)
            {
                case 2:
                    input += "==";
                    break;
                case 3:
                    input += "=";
                    break;
            }

            return Convert.FromBase64String(input);
        }
    }
}
