using Articles.Abstractions.Enums;
using Blocks.Domain.Entities;
using Review.Domain.Assets.ValueObjects;

namespace Review.Domain.Assets;

public class AssetTypeDefinition : EnumEntity<AssetType>
{
    public required FileExtensions AllowedFileExtensions { get; init; }
    public required string DefaultFileExtension { get; init; } = default!;
    public required byte MaxFileSizeInMegabytes { get; init; }
    public required byte MaxAssetCount {get; init;}
}