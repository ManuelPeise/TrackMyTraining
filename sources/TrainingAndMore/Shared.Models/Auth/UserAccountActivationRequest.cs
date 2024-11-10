namespace Shared.Models.Auth
{
    public class UserAccountActivationRequest
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
