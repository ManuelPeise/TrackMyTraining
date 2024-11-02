namespace Shared.Interfaces.UserInterfaces
{
    public interface IUserCredentials
    {
        public string Password { get; set; }
        public string Salt { get; set; }
        public string ExpiresAt { get; set; }
    }
}
