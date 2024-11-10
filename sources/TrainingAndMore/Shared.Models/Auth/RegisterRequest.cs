using Shared.Interfaces.UserInterfaces;

namespace Shared.Models.Auth
{
    public class RegisterRequest : IUserRegistration
    {
        public string Name { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DateOfBirth { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
