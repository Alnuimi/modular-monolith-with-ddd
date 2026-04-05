namespace Articles.Abstractions.Events.Dtos;

public sealed record JournalDto(
    int Id,
    string Abbreviation,
    string Name);