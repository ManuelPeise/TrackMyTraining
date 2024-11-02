using System.ComponentModel.DataAnnotations;
using Web.Core.Resources;

namespace Web.Core.Models.Validation
{
    public class PasswordValidation : ValidationAttribute
    {
        public string PasswordValue { get; set; }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var password = value.ToString();

                if (password == PasswordValue) 
                { 
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(Common.PasswordReplicatiopnError, new[] { validationContext.MemberName });

        }
    }
}
