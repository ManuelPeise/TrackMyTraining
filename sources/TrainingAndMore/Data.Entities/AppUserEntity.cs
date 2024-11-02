using Shared.Enums;
using Shared.Interfaces.UserInterfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class AppUserEntity : AEntityBase, IAppUser
    {
        public string Name { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName => Email;
        public string DateOfBirth { get; set; } = string.Empty;
        public UserRoleEnum UserRole { get; set; }
        public bool IsActive { get; set; }
        public int CredentialsId { get; set; }

        [ForeignKey(nameof(CredentialsId))]
        public AppUserCredentials? Credentials { get; set; }

    }
}
