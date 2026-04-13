
namespace Blocks.Core.Security;

public interface IClaimsProvider
{
    string GetClaimValue(string claimName);
    int GetUserId();
    int? TryGetUserId();
    string GetUserEmail();
    string GetUserName();
    string GetUserRole();
}
