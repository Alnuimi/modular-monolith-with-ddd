namespace Articles.IntegrationEvents.Contracts.Dtos;

public sealed record FileDto(
    string OriginalName,
    string Name,
    string Extension,
    string FileServerId,
    long Size);