namespace Articles.Abstractions.Events.Dtos;

public sealed record FileDto(
    string OriginalName,
    string Name,
    string Extension,
    string FileServerId,
    long Size);