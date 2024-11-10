namespace Shared.Models.Auth
{
    public class UserExportModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string DateOfBirth { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
