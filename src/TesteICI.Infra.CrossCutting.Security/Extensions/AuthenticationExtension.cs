using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Identity.Jwt;
using TesteICI.Infra.CrossCutting.Security.Data;

namespace TesteICI.Infra.CrossCutting.Security.Extensions;

public static class AuthenticationConfig
{
    public static IServiceCollection AddAuthenticationApiConfig(this IServiceCollection services, ConfigurationManager configuration)
    {
        ConfigureCommonAuthentication(services, configuration);

        services.AddJwtConfiguration(configuration);

        services.AddAuthorization();

        services
            .AddJwksManager()
            .UseJwtValidation()
            .PersistKeysToDatabaseStore<SecurityDbContext>();

        return services;
    }

    public static IServiceCollection AddAuthenticationMvcConfig(this IServiceCollection services, ConfigurationManager configuration)
    {
        ConfigureCommonAuthentication(services, configuration);

        services.AddAuthorization();

        return services;
    }

    private static void ConfigureCommonAuthentication(IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddScoped<ISecurityService, SecurityService>();

        services.AddDbContext<SecurityDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
            })
        );

        services.AddDefaultIdentity<IdentityUser>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 5;
            options.Password.RequiredUniqueChars = 1;
        })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<SecurityDbContext>()
            .AddErrorDescriber<IdentityPortugueseMessages>()
            .AddDefaultTokenProviders();
    }
}
