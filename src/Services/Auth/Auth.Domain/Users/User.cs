using Articles.Abstractions.Enums;
using Auth.Domain.Persons;
using Auth.Domain.Persons.ValueObjects;
using Blocks.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Auth.Domain.Users;


public partial class User : IdentityUser<int>, IEntity
{
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    public DateTime? LastLogin  { get; set; }
    
    public int PersonId { get; set; }
    public Person Person { get; set; } = null!;

    private List<UserRole> _userRoles = new List<UserRole>();
    public virtual IReadOnlyList<UserRole> UserRoles => _userRoles.AsReadOnly();

    private List<RefreshToken> _refreshTokens = new List<RefreshToken>();
    public virtual IReadOnlyList<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();
}