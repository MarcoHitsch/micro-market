using Prometheus;
using RestServices.Basket;
using RestServices.Catalog;
using RestServices.Order;
using shared.Dependency;

var builder = WebApplication.CreateBuilder(args);

builder.AddGenericStartup();

builder.Services.AddHttpClient("orderClient", (_, c) => { c.BaseAddress = new Uri(builder.Configuration.GetConnectionString("OrderService")); });
builder.Services.AddHttpClient("basketClient", (_, c) => { c.BaseAddress = new Uri(builder.Configuration.GetConnectionString("BasketService")); });
builder.Services.AddHttpClient("catalogClient", (_, c) => { c.BaseAddress = new Uri(builder.Configuration.GetConnectionString("CatalogService")); });

builder.Services.AddScoped<OrderService>(provider =>
    new OrderService("", provider.GetRequiredService<IHttpClientFactory>().CreateClient("orderClient")));
builder.Services.AddScoped<BasketService>(provider =>
    new BasketService("", provider.GetRequiredService<IHttpClientFactory>().CreateClient("basketClient")));
builder.Services.AddScoped<CatalogService>(provider =>
    new CatalogService("", provider.GetRequiredService<IHttpClientFactory>().CreateClient("catalogClient")));

Console.WriteLine($"Order: {builder.Configuration.GetConnectionString("OrderService")}");
Console.WriteLine($"Basket: {builder.Configuration.GetConnectionString("BasketService")}");
Console.WriteLine($"Catalog: {builder.Configuration.GetConnectionString("CatalogService")}");

var app = builder.Build();

app.AddGenericStartup();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
