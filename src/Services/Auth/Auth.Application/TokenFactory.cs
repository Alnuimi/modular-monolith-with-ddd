using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Auth.Domain.Users;
using Blocks.Core;
using Articles.Security;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

namespace Auth.Application;

public sealed class TokenFactory
{
    private readonly JwtOptions _jwtOptions;
    public TokenFactory(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }
    public RefreshToken GenerateRefreshToken(string clientIpAddress)
    {
       using(var rng = RandomNumberGenerator.Create())
       {
            var randomBytes = new byte[64];
            rng.GetBytes(randomBytes);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                CreatedOn = DateTime.UtcNow,
                ExpiresOn = DateTime.UtcNow.AddDays(7), // Set refresh token expiration time
                CreatedByIp = clientIpAddress
            };
       }
    }
    public string GenerateJwtToken(string userId, string fullName, string email, IEnumerable<string> roles, IEnumerable<Claim> additionalClaims)
    {
        // Implement JWT token generation logic here
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64),

            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, fullName),
            new Claim(ClaimTypes.Email, email),

        }
        .Concat(roles.Select(role => new Claim(ClaimTypes.Role, role)))
        .Concat(additionalClaims);

        var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(_jwtOptions.Secret)); // todo: move to configuration
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var jwtToken = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            notBefore: DateTime.UtcNow,
            expires: _jwtOptions.Expiration,
            claims: claims,
            signingCredentials: signingCredentials
        );

        var encodedJwtToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return encodedJwtToken;
    }
}
