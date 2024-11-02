using Data.Context.Configurations;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Context
{
    public class AppDataContext: DbContext
    {
        private readonly IConfiguration _configuration;
        public AppDataContext(DbContextOptions options, IConfiguration config): base(options)
        {
            _configuration = config;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // builder.Entity<AppUser>()
            //.HasMany(c => c.UserSettings)
            //.WithOne(p => p.User)
            //.HasForeignKey(p => p.UserId);

            builder.ApplyConfiguration(new AdminUserCredentialsConfiguration(_configuration));
            builder.ApplyConfiguration(new AdminUserConfiguration(_configuration));
        }

        public DbSet<AppUserEntity> AppUser { get; set; }
        public DbSet<AppUserCredentials> Credentials { get; set; }
    }
}
