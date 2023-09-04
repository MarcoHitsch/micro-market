using Correlate.AspNetCore;
using Correlate.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;
using Serilog;

namespace shared.Dependency
{
    public static class Startup
    {
        public static WebApplicationBuilder AddGenericStartup(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((builder, config) =>
            {
                config.ReadFrom.Configuration(builder.Configuration);
                Console.WriteLine(config.WriteTo);
            });

            builder.Services.AddCorrelate();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            return builder;
        }

        public static WebApplication AddGenericStartup(this WebApplication app)
        {
            app.UseCorrelate();
            app.UseMetricServer();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpMetrics(options =>
            {
                options.AddCustomLabel("host", context => context.Request.Host.Host);
            });

            return app;
        }
    }
}
