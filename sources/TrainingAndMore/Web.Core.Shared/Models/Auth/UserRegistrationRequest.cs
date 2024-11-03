using Shared.Interfaces.UserInterfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Web.Core.Shared.Resources;

namespace Web.Core.Shared.Models.Auth
{
    public class UserRegistrationRequest : IUserRegistration
    {
        [DataType(DataType.Text)]
        [Display(Name = nameof(Common.LabelEmail), ResourceType = typeof(Common))]
        [Required(ErrorMessageResourceName = nameof(Common.EmailValidationError), ErrorMessageResourceType = typeof(Common))]
        public string Name { get; set; } = string.Empty;
        [DataType(DataType.Text)]
        [Display(Name = nameof(Common.LabelEmail), ResourceType = typeof(Common))]
        [Required(ErrorMessageResourceName = nameof(Common.EmailValidationError), ErrorMessageResourceType = typeof(Common))]
        public string FirstName { get; set; } = string.Empty;
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = nameof(Common.LabelEmail), ResourceType = typeof(Common))]
        [Required(ErrorMessageResourceName = nameof(Common.EmailValidationError), ErrorMessageResourceType = typeof(Common))]
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        [Display(Name = nameof(Common.LabelDateOfBirth), ResourceType = typeof(Common))]
        [Required(ErrorMessageResourceName = nameof(Common.DateValidationError), ErrorMessageResourceType = typeof(Common))]
        public string DateOfBirth { get; set; } = string.Empty;
        [PasswordPropertyText]
        [MinLength(8)]
        [Display(Name = nameof(Common.LabelPassword), ResourceType = typeof(Common))]
        [Required(ErrorMessageResourceName = nameof(Common.PasswordValidationError), ErrorMessageResourceType = typeof(Common))]
        public string Password { get; set; } = string.Empty;
        [PasswordPropertyText]
        [MinLength(8)]
        [Display(Name = nameof(Common.LabelPasswordReplication), ResourceType = typeof(Common))]
        [Required(ErrorMessageResourceName = nameof(Common.PasswordValidationError), ErrorMessageResourceType = typeof(Common))]
        [Compare("Password", ErrorMessageResourceName = nameof(Common.PasswordReplicatiopnError), ErrorMessageResourceType = typeof(Common))]
        public string PasswordReplication { get; set; } = string.Empty;
    }
}
