using Blocks.Core.Constraints;
using Blocks.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

internal sealed class AssetTypeDefinitionEntityConfiguration : IEntityTypeConfiguration<AssetTypeDefinition>
{
    public void Configure(EntityTypeBuilder<AssetTypeDefinition> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Name).IsUnique();

        builder.Property(e => e.Name)
            .HasEnumConvesion()
            .HasMaxLength(MaxLength.C64)
            .IsRequired()
            .HasColumnOrder(1);

        builder.Property(e => e.MaxFileSizeInMb)
            .HasDefaultValue(5); // 5 MB

        builder.Property(e => e.DefaultFileExtension)
            .HasMaxLength(MaxLength.C8)
            .HasDefaultValue("pdf")
            .IsRequired();

        builder.ComplexProperty(
            e => e.AllowedFileExtensions, complexBuilder =>
            {
                var convertor = BuilderExtensions.BuildJsonListConversion<string>();
                complexBuilder.Property(e => e.Extensions)
                    .HasConversion(convertor)
                    .HasColumnName(complexBuilder.Metadata.PropertyInfo!.Name)
                    .IsRequired();
            });
    }
}