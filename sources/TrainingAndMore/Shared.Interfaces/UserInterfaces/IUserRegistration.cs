namespace Shared.Interfaces.UserInterfaces
{
    public interface IUserRegistration
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public string Password { get; set; }
    }
}
