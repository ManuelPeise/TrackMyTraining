using Shared.Enums;

namespace Shared.Interfaces.UserInterfaces
{
    public interface IAppUser
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public UserRoleEnum UserRole { get; set; }
    }
}
