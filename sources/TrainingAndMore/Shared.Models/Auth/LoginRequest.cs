using Shared.Interfaces.UserInterfaces;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shared.Models.Auth
{
    public class LoginRequest: ILoginRequest
    {
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        //[Display(Name ="Email", ResourceType =typeof(Common))]
        //[Required(ErrorMessageResourceName = "EmailError", ErrorMessageResourceType = typeof(Common))]
       
        public string Email { get; set; } = string.Empty;
        [PasswordPropertyText]
        [MinLength(8)]
        //[Display(Name = "Password", ResourceType = typeof(Common))]
        //[Required(ErrorMessageResourceName = "PasswordError", ErrorMessageResourceType = typeof(Common))]
        public string Password { get; set; } = string.Empty;
        public bool Remember { get; set; }
    }
}
