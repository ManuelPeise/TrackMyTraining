using BusinessLogic.Shared.Interfaces;
using BusinessLogic.Shared.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Web.Core.Shared.Services.Interfaces;
using Web.Core.Shared.Services;
using Web.Core.Client.Bundels;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

ServiceLocator.RegisterServices(builder.Services);

var app = builder.Build();

await app.RunAsync();
