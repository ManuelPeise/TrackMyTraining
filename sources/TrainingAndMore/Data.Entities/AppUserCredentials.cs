using Shared.Interfaces.UserInterfaces;

namespace Data.Entities
{
    public class AppUserCredentials : AEntityBase, IUserCredentials
    {
        public string Password { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public string ExpiresAt { get; set; } = string.Empty;
        public int FailedLogins { get; set; }
    }
}
