using BusinessLogic.Shared.Interfaces;
using BusinessLogic.Shared.Services;
using Web.Core.Shared.Services;
using Web.Core.Shared.Services.Interfaces;

namespace Web.Core.Client.Bundels
{
    internal static class ServiceLocator
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped(typeof(IResourceService<>), typeof(ResourceService<>));
        }
    }
}
