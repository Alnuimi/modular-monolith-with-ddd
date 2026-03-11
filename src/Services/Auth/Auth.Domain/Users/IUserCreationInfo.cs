using Articles.Abstractions.Enums;
using Auth.Domain.Users.Enums;

namespace Auth.Domain.Users;

public interface IUserCreationInfo
{
    string Email { get;  }
    string FirstName { get;  }
    string LastName { get;  }
    Gender Gender { get;  } 
    Honorific? Honorific { get; }
    string? PhoneNumber { get; }
    string? PictureUrl { get; }
    string? CompanyName { get; }
    string? Position { get; }
    string? Affiliation { get;  }
    IReadOnlyList<IUserRole> UserRoles { get;  }
}

public interface IUserRole
{
    DateTime? ExpiringDate { get;  }
    DateTime? StartDate { get; }
    UserRoleType Type { get; }
}