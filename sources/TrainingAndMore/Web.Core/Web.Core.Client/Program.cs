using BusinessLogic.Shared.Interfaces;
using BusinessLogic.Shared.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Web.Core.Shared.Services.Interfaces;
using Web.Core.Shared.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped(typeof(IResourceService<>), typeof(ResourceService<>));

var app = builder.Build();

await app.RunAsync();
