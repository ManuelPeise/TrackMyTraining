using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Data.Context.Configurations
{
    public class AdminUserCredentialsConfiguration: IEntityTypeConfiguration<AppUserCredentials>
    {
        private readonly IConfiguration _configuration;
        public AdminUserCredentialsConfiguration(IConfiguration config)
        {
            _configuration = config;
        }

        public void Configure(EntityTypeBuilder<AppUserCredentials> builder)
        {
            var createdAt = DateTime.Now.ToString("yyyy-MM-dd");
            var salt = Guid.NewGuid().ToString();

            builder.HasData(new AppUserCredentials
            {
                Id = 1,
                RefreshToken = "",
                FailedLogins = 0,
                Password = GetEncodedSecret(_configuration["DefaultUser:Password"], salt),
                Salt = salt,
                CreatedAt = createdAt,
                CreatedBy = "System",
                UpdatedAt = null,
                UpdatedBy = null,
            });
        }

        private string GetEncodedSecret(string secret, string salt)
        {
            var bytes = Encoding.UTF8.GetBytes(secret).ToList();
            bytes.AddRange(Encoding.UTF8.GetBytes(salt));

            return Convert.ToBase64String(bytes.ToArray());
        }
    }
}
