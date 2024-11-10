using Web.Core.Api.Bundles;

var builder = WebApplication.CreateBuilder(args);

AppConfiguration.ConfigureServices(builder);

var app = builder.Build();

AppConfiguration.ConfigureApp(app);

app.Run();
