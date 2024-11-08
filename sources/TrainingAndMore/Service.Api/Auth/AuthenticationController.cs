using BusinessLogic.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Api;
using Shared.Models.Auth;

namespace Service.Api.Auth
{
    public class AuthenticationController : MeasuredApiControllerBase
    {
        private IAuthenticationService _authenticationService;
        private IMetricService _metricService;
        public AuthenticationController(IAuthenticationService authenticationService, IMetricService metricService) : base(metricService)
        {
            _authenticationService = authenticationService;
            _metricService = metricService;
        }

        [HttpPost(Name = "Authentication")]
        public async Task<ApiResponse<string>> UserLogin([FromBody] LoginRequest request)
        {
            var result = await _authenticationService.TryLogIn(request);

            await MonitorEndpointPerformance("Authentication", "Authentication", result);

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

            await MonitorEndpointPerformance("Authentication", "UserRegistration", result);

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

            await MonitorEndpointPerformance("Authentication", "ConfirmEmail", result);

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

            await MonitorEndpointPerformance("Authentication", "UserLogout", result);

            return new ApiResponse<string>
            {
                Success = result,
                Data = result ? "logged out" : string.Empty
            };
        }
    }
}
