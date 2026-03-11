namespace Articles.Security;

public sealed class JwtOptions
{
    public required string Issuer {get; init; }
    public required string Audience {get; init; }
    public required string Secret  {get; init; }
    public int ValidForInMinutes { get; init; }
    public DateTime IssuedAt { get; init; } =  DateTime.UtcNow;
    public TimeSpan ValidFor => TimeSpan.FromMinutes(ValidForInMinutes);
    public DateTime Expiration => IssuedAt.Add(ValidFor);
}