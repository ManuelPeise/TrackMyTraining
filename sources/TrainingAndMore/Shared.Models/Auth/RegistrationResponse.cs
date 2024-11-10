namespace Shared.Models.Auth
{
    public class RegistrationResponse
    {
        public bool Success { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
    }
}
