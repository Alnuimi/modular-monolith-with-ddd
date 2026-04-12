using Articles.Abstractions.Enums;

namespace Articles.IntegrationEvents.Contracts.Dtos;

public sealed record ArticleDto(
    int Id,
    string Title,
    string Scope,
    string? Doi,
    ArticleType Type,
    ArticleStage Stage,
    PersonDto SubmittedBy,
    DateTime SubmittedOn,
    JournalDto Journal,
    List<ActorDto> Actors,
    List<AssetDto> Assets);