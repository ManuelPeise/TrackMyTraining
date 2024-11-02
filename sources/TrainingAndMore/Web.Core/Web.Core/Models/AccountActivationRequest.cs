namespace Web.Core.Models
{
    public class AccountActivationRequest
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
