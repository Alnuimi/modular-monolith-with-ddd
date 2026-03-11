using System.Security.Claims;
using Articles.Security;
using Auth.Domain.Users;
using Auth.Persistence;
using Blocks.Core.Extensions;
using EmailService.Smtp;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Identity;

namespace Auth.API;

public static class DependenciesInjection
{
    public static IServiceCollection ConfigureApiOptions(this IServiceCollection services, IConfiguration configuration)
    {
        // use it for configuring the options
        services
            .AddAndValidateOptions<JwtOptions>(configuration);
        
        return services;
    }

    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddFastEndpoints()
            .SwaggerDocument()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddJwtIdentity(config);
        
        services.AddSmtpEmailService(config);

        return services;
    }

    private static IServiceCollection AddJwtIdentity(this IServiceCollection services, IConfiguration config)
    {
        services.AddIdentityCore<User>(options =>
            {
                // Lockout settings
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;

                // options.User.AllowedUserNameCharacters = "abcd";
            })
            .AddRoles<Auth.Domain.Roles.Role>()
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddSignInManager<SignInManager<User>>()
            .AddDefaultTokenProviders();
        
        services.Configure<IdentityOptions>(options =>
        {
            options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
        });

        return services;
    }
}