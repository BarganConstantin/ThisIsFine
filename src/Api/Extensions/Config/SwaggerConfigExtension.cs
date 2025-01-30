using NSwag;
using NSwag.Generation.Processors.Security;
using ZymLabs.NSwag.FluentValidation;

namespace ThiIsFine.Api.Extensions.Config
{
    public static class SwaggerConfigExtension
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddOpenApiDocument((configure, sp) =>
            {
                configure.Title = "This Is Fine API";

                // Add the fluent validations schema processor
                var fluentValidationSchemaProcessor = 
                    sp.CreateScope().ServiceProvider.GetRequiredService<FluentValidationSchemaProcessor>();

                configure.SchemaSettings.SchemaProcessors.Add(fluentValidationSchemaProcessor);

                // Add JWT
                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });

            return services;
        }
    }
}
