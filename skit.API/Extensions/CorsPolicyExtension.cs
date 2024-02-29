using skit.Shared.Configurations.Cors;

namespace skit.API.Extensions;

public static class CorsPolicyExtension
{
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        var corsOptions = new CorsConfig();
        configuration.GetSection("Cors").Bind(corsOptions);

        return services
            .AddSingleton(corsOptions)
            .AddCors(cors =>
            {
                var allowedHeaders = corsOptions.AllowedHeaders;
                var allowedMethods = corsOptions.AllowedMethods;
                var allowedOrigins = corsOptions.AllowedOrigins;
                var exposedHeaders = corsOptions.ExposedHeaders;
                cors.AddPolicy("CorsPolicy", corsBuilder =>
                {
                    if (corsOptions.AllowCredentials)
                    {
                        corsBuilder.AllowCredentials();
                    }
                    else
                    {
                        corsBuilder.DisallowCredentials();
                    }

                    corsBuilder
                        .WithOrigins(allowedOrigins.ToArray())
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
    } 
}