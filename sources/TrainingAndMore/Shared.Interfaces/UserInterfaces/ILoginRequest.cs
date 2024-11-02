namespace Shared.Interfaces.UserInterfaces
{
    public interface ILoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}
