using ThiIsFine.Infrastructure.Data;
using ZymLabs.NSwag.FluentValidation;

namespace ThiIsFine.Api.Extensions.Config
{
    public static class WebConfigExtension
    {
        public static IServiceCollection AddWebConfig(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddHttpContextAccessor();
            
            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

            services.AddScoped(provider =>
            {
                var validationRules = provider.GetService<IEnumerable<FluentValidationRule>>();
                var loggerFactory = provider.GetService<ILoggerFactory>();

                return new FluentValidationSchemaProcessor(provider, validationRules, loggerFactory);
            });
            
            return services;
        }
    }
}
