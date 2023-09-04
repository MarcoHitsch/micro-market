using basket_service.Data;
using basket_service.Services;
using shared.Dependency;

var builder = WebApplication.CreateBuilder(args);
builder.AddGenericStartup();
builder.Services.AddGrpc();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddScoped<CatalogService>(_ => new CatalogService(builder.Configuration.GetConnectionString("CatalogService")));

var app = builder.Build();

app.AddGenericStartup();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
