namespace Web.Core.Shared.Models.Auth
{
    public class AccountActivationRequestModel
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
