using Articles.Abstractions.Enums;

namespace Articles.Abstractions.Events.Dtos;

public sealed record AssetDto(
    int Id,
    string Name,
    AssetType Type,
    FileDto File);