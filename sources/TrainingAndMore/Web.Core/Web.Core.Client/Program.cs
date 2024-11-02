using BusinessLogic.Shared.Interfaces;
using BusinessLogic.Shared.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<IHttpService, HttpService>();

var app = builder.Build();

await app.RunAsync();
