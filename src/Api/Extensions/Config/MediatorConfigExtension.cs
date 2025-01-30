using System.Reflection;
using FluentValidation;
using ThiIsFine.Application;

namespace ThiIsFine.Api.Extensions.Config
{
    public static class MediatorConfigExtension
    {
        public static IServiceCollection AddMediatrConfig(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<MediatorEntryPoint>();
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            return services;
        }
    }
}
