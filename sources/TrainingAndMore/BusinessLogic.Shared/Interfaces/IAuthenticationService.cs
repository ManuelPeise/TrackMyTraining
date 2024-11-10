using Shared.Models.Auth;

namespace BusinessLogic.Shared.Interfaces
{
    public interface IAuthenticationService: IDisposable
    {
        Task<string?> TryLogIn(LoginRequest loginRequest);
        Task<bool> TryLogOut(int userId);
        Task<RegistrationResponse> TryRegisterUser(RegisterRequest registerRequest);
        Task<UserAccountActivationResponse?> TryActivateAccount(UserAccountActivationRequest request);
    }
}
