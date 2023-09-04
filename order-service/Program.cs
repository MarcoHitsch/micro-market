using catalog_service_api.Events;
using order_service.Events;
using order_service.Data;
using shared.Events;
using EasyNetQ;
using shared.Dependency;

var builder = WebApplication.CreateBuilder(args);
builder.AddGenericStartup();

builder.Services.Configure<ConnectionOptions>(builder.Configuration.GetSection("ConnectionOptions"));
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddSingleton<IBus>(RabbitHutch.CreateBus(builder.Configuration.GetConnectionString("RabbitMq")));
builder.Services.AddScoped<Subscriber>();
builder.Services.AddScoped<CatalogEventHandler>();

var app = builder.Build();
app.AddGenericStartup();

if (!(app.Environment.IsEnvironment("Build")))
{
    using var serviceScope = app.Services.CreateScope();
    var services = serviceScope.ServiceProvider;

    var subscriber = services.GetRequiredService<Subscriber>();
    var catalogEventHandler = services.GetRequiredService<CatalogEventHandler>();
    
    try{
      await subscriber.Subscribe<ProductPriceChanged>(async p =>
          {
              Console.WriteLine($"PriceChanged aus order-service");
              await catalogEventHandler.HandleProductChanged(p);
          }, "order-service");
    } catch {
      System.Console.WriteLine("Could not subscribe properly, service might not work correctly");
    }    
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
