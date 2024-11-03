using BusinessLogic.Shared.Interfaces;
using BusinessLogic.Shared.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Web.Core.Components;
using Web.Core.Shared.Providers;
using Web.Core.Shared.Services;
using Web.Core.Shared.Services.Interfaces;

string[] supportedCultures = new string[] { "en-En","de-De" };
// D:\Development\WorkFolder\TrainingTracker\TrackMyTraining\sources\TrainingAndMore\Web.Core.Shared\Resources\Common.resx
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization();

builder.Services.Configure<RequestLocalizationOptions>(opt =>
{
    opt.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-En");
    opt.SupportedCultures = supportedCultures.Select(x => new System.Globalization.CultureInfo(x)).ToList();
});

builder.Services.AddControllers();

builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped(typeof(IResourceService<>), typeof(ResourceService<>));

builder.Services.AddAuthenticationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddCascadingAuthenticationState();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();



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
