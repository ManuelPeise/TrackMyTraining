using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Shared.Interfaces.UserInterfaces;
using Web.Core.Resources;

namespace Web.Core.Models
{
    public class LoginRequest: ILoginRequest
    {
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = nameof(Common.LabelEmail), ResourceType = typeof(Common))]
        [Required(ErrorMessageResourceName = nameof(Common.EmailValidationError), ErrorMessageResourceType = typeof(Common))]

        public string Email { get; set; } = string.Empty;
        [PasswordPropertyText]
        [MinLength(8)]
        [Display(Name = nameof(Common.LabelPassword), ResourceType = typeof(Common))]
        [Required(ErrorMessageResourceName = nameof(Common.PasswordValidationError), ErrorMessageResourceType = typeof(Common))]
        public string Password { get; set; } = string.Empty;
        public bool Remember { get; set; }
    }
}
