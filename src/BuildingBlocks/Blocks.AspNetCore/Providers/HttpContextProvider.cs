
using System.Security.Claims;
using Blocks.Core.Extensions;
using Blocks.Core.Security;
using Microsoft.AspNetCore.Http;

namespace Blocks.AspNetCore.Providers;

public sealed class HttpContextProvider(IHttpContextAccessor _httpContextAccessor) : IClaimsProvider
{
    public string GetClaimValue(string claimName)
        => TryGetClaimValue(claimName) ?? throw new InvalidOperationException($"Missing claim: {claimName}.");

    public string? TryGetClaimValue(string claimName)
        =>  _httpContextAccessor.GetClaimValue(claimName);

    public string GetUserEmail()
        => GetClaimValue(ClaimTypes.Email);

    public int GetUserId()
        => TryGetUserId() ?? throw new UnauthorizedAccessException($"Missing claim: {ClaimTypes.NameIdentifier}.");
    public int? TryGetUserId()
        => TryGetClaimValue(ClaimTypes.NameIdentifier)?.ToInt();

    public string GetUserName()
        => GetClaimValue(ClaimTypes.NameIdentifier);

    public string GetUserRole()
        => GetClaimValue(ClaimTypes.Role);

   
}
