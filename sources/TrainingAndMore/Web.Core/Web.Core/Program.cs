using BusinessLogic.Shared.Interfaces;
using BusinessLogic.Shared.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Web.Core.Components;
using Web.Core.Providers;

string[] supportedCultures = new string[] { "en-En","de-De" };

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization(opt => opt.ResourcesPath = "~Web.Core\\Resources\\");

builder.Services.Configure<RequestLocalizationOptions>(opt =>
{
    opt.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-En");
    opt.SupportedCultures = supportedCultures.Select(x => new System.Globalization.CultureInfo(x)).ToList();
});

builder.Services.AddControllers();

builder.Services.AddScoped<IHttpService, HttpService>();


builder.Services.AddAuthenticationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
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
