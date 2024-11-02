using BusinessLogic.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Api;
using Shared.Models.Auth;

namespace Service.Api.Auth
{
    public class AuthenticationController : ApiControllerBase
    {
        private IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost(Name = "UserLogin")]
        public async Task<ApiResponse<string>> UserLogin([FromBody] LoginRequest request)
        {
            var result = await _authenticationService.TryLogIn(request);

            return new ApiResponse<string>
            {
                Success = !string.IsNullOrEmpty(result),
                Data = result
            };
        }

        [HttpPost(Name = "UserRegistration")]
        public async Task<ApiResponse<RegistrationResponse>> UserRegistration([FromBody] RegisterRequest request)
        {
            var result = await _authenticationService.TryRegisterUser(request);

            return new ApiResponse<RegistrationResponse>
            {
                Success = result?.Success ?? false,
                Data = result ?? null
            };
        }

        [HttpPost(Name = "ConfirmEmail")]
        public async Task<ApiResponse<UserAccountActivationResponse>> ConfirmEmail([FromBody] UserAccountActivationRequest request)
        {
            var result = await _authenticationService.TryActivateAccount(request);

            return new ApiResponse<UserAccountActivationResponse>
            {
                Success = result != null,
                Data = result ?? null
            };
        }

        [HttpGet("{userId}", Name = "UserLogout")]
        public async Task<ApiResponse<string>> UserLogout(int userId)
        {
            var result = await _authenticationService.TryLogOut(userId);

            return new ApiResponse<string>
            {
                Success = result,
                Data = result ? "logged out" : string.Empty
            };
        }
    }
}
