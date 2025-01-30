using Application.Core.Services.DateTimeProvider;
using Application.Core.Services.Identity;
using Application.Core.Services.ImageUpload;
using Application.Core.Services.TokenGenerator;
using Application.Core.Services.UserInfo;
using ThiIsFine.Api.ResponseMapper;
using ThiIsFine.Infrastructure.Identity;
using ThiIsFine.Infrastructure.Services.DateTimeProvider;
using ThiIsFine.Infrastructure.Services.ImageUpload;
using ThiIsFine.Infrastructure.Services.TokenGenerator;
using ThiIsFine.Infrastructure.Services.UserInfo;

namespace ThiIsFine.Api.Extensions.Config
{
    public static class ServicesConfigExtension
    {
        public static IServiceCollection AddServicesConfig(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IResponseMapper, HttpResponseMapper>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddTransient<ICurrentUserInfo, CurrentUserInfo>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            //services.AddSingleton<IImageUploadService, CloudinaryImageUploadService>();
            services.AddScoped<IImageUploadService, DatabaseImageUploadService>();

            services.AddMemoryCache();

            return services;
        }
    }
}
