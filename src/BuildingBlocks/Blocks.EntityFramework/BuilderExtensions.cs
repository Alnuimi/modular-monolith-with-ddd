using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Blocks.EntityFramework;

public static class BuilderExtensions
{
    public static PropertyBuilder<TEnum> HasEnumConvesion<TEnum>(this PropertyBuilder<TEnum> builder)
        where TEnum : Enum
    {
        return  builder
            .HasConversion(
                v => v.ToString(),
                value => (TEnum)Enum.Parse(typeof(TEnum), value)
            );   
    }

    public static PropertyBuilder<T> HasJsonCollectionConversion<T>(this PropertyBuilder<T> builder)
    {
        return builder.HasConversion(BuildJsonListConversion<T>());
    }

    public static ValueConverter<TCollection, string> BuildJsonListConversion<TCollection>()
    {
        Func<TCollection, string> serializeFunc = v => JsonSerializer.Serialize(v);
        Func<string, TCollection> deserializeFunc = v => JsonSerializer.Deserialize<TCollection>(v ?? "[]");

        return new ValueConverter<TCollection, string>(
            v => serializeFunc(v),
            v => deserializeFunc(v)
        );
    }
}
