using BusinessLogic.Shared.Interfaces;
using BusinessLogic.Shared.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Web.Core.Components;
using Web.Core.Shared.Providers;
using Web.Core.Shared.Services.Interfaces;
using Web.Core.Shared.Services;
using Web.Core.Shared.ViewModels.Interfaces;
using Web.Core.Shared.ViewModels;
using Web.Core.Bundles;

string[] supportedCultures = new string[] { "en-En", "de-De" };

var builder = WebApplication.CreateBuilder(args);

ServiceLocator.RegisterServices(builder.Services, supportedCultures);


var app = builder.Build();

app.UseRequestLocalization(new RequestLocalizationOptions()
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Web.Core.Client._Imports).Assembly);

app.UseStatusCodePagesWithRedirects("/no-content");

app.Run();
