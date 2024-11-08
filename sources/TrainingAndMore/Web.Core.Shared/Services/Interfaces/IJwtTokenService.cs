namespace Web.Core.Shared.Services.Interfaces
{
    public interface IJwtTokenService
    {
        Task<string?> GetToken();
    }
}
