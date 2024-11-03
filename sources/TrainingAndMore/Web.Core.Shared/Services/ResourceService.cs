using Microsoft.Extensions.Localization;
using Web.Core.Shared.Services.Interfaces;

namespace Web.Core.Shared.Services
{
    public class ResourceService<T> : IResourceService<T> where T : class
    {
        private readonly IStringLocalizer<T> _localizer;

        public ResourceService(IStringLocalizer<T> localizer)
        {
            _localizer = localizer;
        }

        public string GetResource(string key)
        {
            var value = _localizer[key];

            return value;
        }
    }
}
