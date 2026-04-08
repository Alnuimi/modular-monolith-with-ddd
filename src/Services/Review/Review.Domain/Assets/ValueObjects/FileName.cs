using Blocks.Domain.ValueObjects;

namespace Review.Domain.Assets.ValueObjects;

public class FileName : StringValueObject
{
    private FileName(string value) => Value = value;

    public static FileName FromAsset(Asset asset, FileExtension extension)
    {
        var assetName = asset.Name.Value.Replace("'", "").Replace(" ", "-");
        return new FileName($"{assetName}.{extension}");
    }

    public static FileName Create(string name)
    {
        return new FileName(name);
    }
}