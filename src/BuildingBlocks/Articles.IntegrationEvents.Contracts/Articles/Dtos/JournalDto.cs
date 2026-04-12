namespace Articles.IntegrationEvents.Contracts.Dtos;

public sealed record JournalDto(
    int Id,
    string Abbreviation,
    string Name);