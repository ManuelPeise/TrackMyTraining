using BusinessLogic.Shared.Interfaces;
using BusinessLogic.Shared.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Web.Core.Shared.Providers;
using Web.Core.Shared.Services.Interfaces;
using Web.Core.Shared.Services;
using Web.Core.Shared.ViewModels.Interfaces;
using Web.Core.Shared.ViewModels;

namespace Web.Core.Bundles
{
    internal static class ServiceLocator
    {
        internal static void RegisterServices(IServiceCollection services, string[] supportedCultures)
        {
            // cultures
            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(opt =>
            {
                opt.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-En");
                opt.SupportedCultures = supportedCultures.Select(x => new System.Globalization.CultureInfo(x)).ToList();
            });

            services.AddControllers();

            // general servies
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped(typeof(IResourceService<>), typeof(ResourceService<>));

            // view models
            services.AddScoped<IApiPerformanceViewModel, ApiPerformanceViewModel>();

            // authentication
            services.AddAuthenticationCore();
            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            services.AddCascadingAuthenticationState();

            services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();
        }
    }
}
