using BusinessLogic.Administration.Services;
using BusinessLogic.Shared.Interfaces;
using BusinessLogic.Shared.Services;
using Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Web.Core.Api.Bundles
{
    public static class AppConfiguration
    {
        private const string CorsPolicy = "cors-policy";

        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDataContext>(opt =>
            {
                var connection = builder.Configuration.GetConnectionString("AppDataContext") ?? null;

                if (connection == null)
                {
                    throw new Exception("Could not configure database context!");
                }

                opt.UseMySQL(connection);
            });

            

            builder.Services.AddScoped<IMetricService, MetricService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IApiPerformanceService, ApiPerformanceService>();

            ConfigureJwt(builder);

            builder.Services.AddCors((opt) =>
            {
                opt.AddPolicy(CorsPolicy, (opt) =>
                {
                    opt.AllowAnyHeader();
                    opt.AllowAnyMethod();
                    opt.AllowAnyOrigin();
                });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }

        public static void ConfigureApp(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseCors(CorsPolicy);

            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                Thread.CurrentPrincipal = context.User;
                await next(context);
            });

            app.MapControllers();
        }

        private static void ConfigureJwt(WebApplicationBuilder builder)
        {
            var (issuer, key) = GetJwtDataFromConfig(builder);

            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                });
        }

        private static (string? jwtIssuer, string? jwtKey) GetJwtDataFromConfig(WebApplicationBuilder builder)
        {
            var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
            var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

            return (jwtIssuer, jwtKey);
        }

        //private static void ExecuteDatabaseMigrations(WebApplication app)
        //{
        //    using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        //    {
        //        var context = serviceScope.ServiceProvider.GetService<TrainingDbContext>();

        //        if (context != null)
        //        {
        //            if (context.Database.GetPendingMigrations().Any())
        //            {
        //                context.Database.Migrate();
        //            }

        //        }
        //    }
        //}
    }
}
