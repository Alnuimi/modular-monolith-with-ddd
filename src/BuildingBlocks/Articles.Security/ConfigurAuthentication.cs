
using System.Security.Claims;
using System.Text;
using Blocks.Core.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Articles.Security;

public static class ConfigurAuthentication
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSectionByTypeName<JwtOptions>();
        services.AddSingleton(jwtOptions);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.RequireHttpsMetadata = false;
                jwtBearerOptions.SaveToken = true;
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(jwtOptions.Secret)),
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    RoleClaimType = ClaimTypes.Role
                };
            });

        return services;
    }
}
