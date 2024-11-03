using Microsoft.AspNetCore.Components;
using Web.Core.Shared.Resources;
using Web.Core.Shared.Services.Interfaces;

namespace Web.Core.Components.Pages.SystemPages
{
    public partial class NoContentPage
    {
        [Inject]
        public IResourceService<Common>? ResourceService { get; set; }
    }
}
