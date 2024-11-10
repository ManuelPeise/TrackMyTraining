using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Data.Context.Configurations
{
    public class AdminUserConfiguration : IEntityTypeConfiguration<AppUserEntity>
    {
        private readonly IConfiguration _configuration;

        public AdminUserConfiguration(IConfiguration config)
        {
            _configuration = config;
        }

        public void Configure(EntityTypeBuilder<AppUserEntity> builder)
        {
            var createdAt = DateTime.Now.ToString("yyyy-MM-dd");
           
            var dateOfBirth = _configuration["DefaultUser:DateOfBirth"] == null
                ? DateTime.Now
                : DateTime.Parse((string)_configuration["DefaultUser:DateOfBirth"]);

            var userRole = (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), _configuration["DefaultUser:UserRole"]);

            builder.HasData(new AppUserEntity
            {
                Id = 1,
                Name = _configuration["DefaultUser:Name"],
                FirstName = _configuration["DefaultUser:FirstName"],
                DateOfBirth = dateOfBirth.ToString("dd.MM.yyyy"),
                Email = _configuration["DefaultUser:Email"],
                UserRole = userRole,
                IsActive = true,
                CredentialsId = 1,
                CreatedAt = createdAt,
                CreatedBy = "System",
                UpdatedAt = null,
                UpdatedBy = null,
            });
        }
    }
}
