namespace Articles.Abstractions.Events.Dtos;

public sealed record PersonDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string? Honorific,
    string? Affiliation,
    int? UserId,
    string TypeDiscriminator);