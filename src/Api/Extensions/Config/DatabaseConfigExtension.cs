using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ThiIsFine.Application.Repositories;
using ThiIsFine.Infrastructure.Data;
using ThiIsFine.Infrastructure.Identity;
using ThiIsFine.Infrastructure.Repositories;
using ThiIsFine.Infrastructure.Repositories.UnitsOfWork;

namespace ThiIsFine.Api.Extensions.Config
{
    public static class DatabaseConfigExtension
    {
        public static IServiceCollection ConfigureDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.SetupRepositories();
            services.SetupIdentityDatabase(configuration);

            return services;
        }

        private static IServiceCollection SetupRepositories(this IServiceCollection services)
        {
            services.AddScoped<IApplicationUnitOfWork, ApplicationUnitOfWork>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
            services.AddScoped<IUsagesRepository, UsagesRepository>();
            services.AddScoped<IPurchasesRepository, PurchasesRepository>();
            
            return services;
        }

        private static IServiceCollection SetupIdentityDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");
            
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<ApplicationDbContextInitialiser>();
            
            services
                .AddIdentityCore<ThisIsFineUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddApiEndpoints();

            return services;
        }
    }
}
