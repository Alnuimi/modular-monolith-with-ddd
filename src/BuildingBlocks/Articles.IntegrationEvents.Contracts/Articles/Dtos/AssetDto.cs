using Articles.Abstractions.Enums;

namespace Articles.IntegrationEvents.Contracts.Dtos;

public sealed record AssetDto(
    int Id,
    string Name,
    AssetType Type,
    FileDto File);