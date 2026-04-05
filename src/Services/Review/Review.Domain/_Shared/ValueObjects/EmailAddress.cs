using System.Text.RegularExpressions;
using Blocks.Core;
using Blocks.Domain.ValueObjects;

namespace Review.Domain.Shared.ValueObjects;

public class EmailAddress : StringValueObject
{
    private EmailAddress(string value)
    {
        Value = value;
        NormalizedEmail = value.ToUpperInvariant();
    }
    public string NormalizedEmail { get; internal set; }
    
    public static EmailAddress Create(string value)
    {
        Guard.ThrowIfNullOrWhiteSpace(value);
        Guard.ThrowIfFalse(!IsValidEmail(value), "Invalid email format.");
        
        
        return new (value); 
    }
    private static bool IsValidEmail(string email)
    {
        const string emailRegex = @"[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailRegex, RegexOptions.IgnoreCase);
    }
    
    public static implicit operator EmailAddress(string value)
    {
        return Create(value);
    }
    
    public static implicit operator string(EmailAddress email)
    {
        return email.Value;
    }
    
    public static bool operator ==(EmailAddress a, string b)
    {
        if (ReferenceEquals(a, null) && b == null) return true;
        if (ReferenceEquals(a, null) || b == null) return false;
        
        return string.Equals(a.Value, b, StringComparison.OrdinalIgnoreCase);
    }

    public static bool operator !=(EmailAddress a, string b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return StringComparer.OrdinalIgnoreCase.GetHashCode(Value);
    }
}