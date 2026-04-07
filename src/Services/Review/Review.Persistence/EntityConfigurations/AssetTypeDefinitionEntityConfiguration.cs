using Articles.Abstractions.Enums;
using Blocks.Core.Constraints;
using Blocks.EntityFramework;
using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Domain.Assets;

namespace Review.Persistence.EntityConfigurations;

internal sealed class AssetTypeDefinitionEntityConfiguration : EnumEntityConfiguration<AssetTypeDefinition,AssetType>
{
    public override void Configure(EntityTypeBuilder<AssetTypeDefinition> builder)
    {
        base.Configure(builder);

        builder.Property(a => a.MaxAssetCount).HasDefaultValue(1);

        builder.Property(a => a.DefaultFileExtension)
            .HasDefaultValue("pdf")
            .IsRequired()
            .HasMaxLength(MaxLength.C8);

        builder.ComplexProperty(
            o => o.AllowedFileExtensions, propertyBuilder =>
            {
                var convertor = BuilderExtensions.BuildJsonReadOnlyListConversion<string>();
                propertyBuilder.Property(e => e.Extensions)
                    .HasConversion(convertor)
                    .HasColumnName(propertyBuilder.Metadata.PropertyInfo!.Name)
                    .IsRequired();
            });
    }
}