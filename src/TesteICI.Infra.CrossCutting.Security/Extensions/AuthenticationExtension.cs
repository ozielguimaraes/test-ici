using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Identity.Jwt;
using TesteICI.Infra.CrossCutting.Security.Data;

namespace TesteICI.Infra.CrossCutting.Security.Extensions;

public static class AuthenticationConfig
{
    public static IServiceCollection AddAuthenticationConfig(this IServiceCollection services, ConfigurationManager configuration)
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
        //services.AddIdentityConfiguration();
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

        services.AddJwtConfiguration(configuration);

        // Adicionar configurações do JWT
        //var jwtSettings = configuration.GetSection("AppJwtSettings").Get<JwtSetting>();
        //ArgumentNullException.ThrowIfNull(jwtSettings);

        //// Configurar a autenticação JWT
        //services.AddAuthentication(options =>
        //{
        //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //})
        //.AddJwtBearer(options =>
        //{
        //    options.TokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateIssuer = true,
        //        ValidateAudience = true,
        //        ValidateLifetime = true,
        //        ValidateIssuerSigningKey = true,
        //        ValidIssuer = jwtSettings.Issuer,
        //        ValidAudience = jwtSettings.Audience,
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        //    };
        //});

        services.AddAuthorization();

        services
            .AddJwksManager()
            .UseJwtValidation()
            .PersistKeysToDatabaseStore<SecurityDbContext>();

        return services;
    }
}
