using catalog_service.Data;
using shared.Data;
using Microsoft.EntityFrameworkCore;
using EasyNetQ;
using shared.Events;
using catalog_service.Controllers;
using Prometheus;
using shared.Dependency;

var builder = WebApplication.CreateBuilder(args);

builder.AddGenericStartup();
builder.Services.AddGrpc();

builder.Services.AddDbContext<ApplicationDbContext>((provider, options) =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PgDatabase"))
           .UseLazyLoadingProxies();
});

builder.Services.AddSingleton<IBus>(RabbitHutch.CreateBus(builder.Configuration.GetConnectionString("RabbitMq")));
builder.Services.AddScoped<Publisher>();

var app = builder.Build();

app.AddGenericStartup();

if (!app.Environment.IsEnvironment("Build"))
{
    using var serviceScope = app.Services.CreateScope();
    var services = serviceScope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();
    context.EnsureDatabaseMigration();
}

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGrpcService<ProductControllerGrpc>();
});

app.Run();
