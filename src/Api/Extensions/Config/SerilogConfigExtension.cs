using Serilog;

namespace ThiIsFine.Api.Extensions.Config
{
    public static class SerilogConfigExtension
    {
        public static IServiceCollection AddSerilogConfiguration(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));

            return services;
        }
    }
}
