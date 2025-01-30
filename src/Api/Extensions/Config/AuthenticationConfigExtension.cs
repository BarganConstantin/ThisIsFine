using System.Security.Cryptography;
using Application.Core.Services.RsaKeyGenerator;
using Domain.Core.Constants;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ThiIsFine.Infrastructure.Services.RsaKeyGenerator;

namespace ThiIsFine.Api.Extensions.Config
{
    public static class AuthenticationConfigExtension
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            byte[] key;
            var keyFilePath = config["JwtSettings:FileWithKeyPath"];

            if (File.Exists(keyFilePath))
            {
                key = File.ReadAllBytes(keyFilePath);
            }
            else
            {
                IRsaKeyGeneratorService rsaKeyGeneratorService = new RsaKeyGeneratorService();
                rsaKeyGeneratorService.GenerateAndSavePrivateKey(keyFilePath!);
                key = File.ReadAllBytes(keyFilePath!);
            }

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                var rsaKey = RSA.Create();
                rsaKey.ImportRSAPrivateKey(key, out _);
                var securityKey = new RsaSecurityKey(rsaKey);

                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = config["JwtSettings:Issuer"],
                    ValidAudience = config["JwtSettings:Audience"],
                    IssuerSigningKey = securityKey,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AccessPolicy.UserAccessPolicy, policy =>
                {
                    policy.RequireRole(Roles.User, Roles.Moderator, Roles.Admin, Roles.SuperAdmin);
                });
                options.AddPolicy(AccessPolicy.ModeratorAccessPolicy, policy =>
                {
                    policy.RequireRole(Roles.Moderator, Roles.Admin, Roles.SuperAdmin);
                });
                options.AddPolicy(AccessPolicy.AdminAccessPolicy, policy =>
                {
                    policy.RequireRole(Roles.Admin, Roles.SuperAdmin);
                });
                options.AddPolicy(AccessPolicy.SuperAdminAccessPolicy, policy =>
                {
                    policy.RequireRole(Roles.SuperAdmin);
                });
            });

            return services;
        }
    }
}
