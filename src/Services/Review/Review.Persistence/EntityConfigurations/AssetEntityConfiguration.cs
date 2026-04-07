using Blocks.Core.Constraints;
using Blocks.EntityFramework;
using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Domain.Assets;

namespace Review.Persistence.EntityConfigurations;

internal sealed class AssetEntityConfiguration : EntityConfiguration<Asset>
{
    public override void Configure(EntityTypeBuilder<Asset> builder)
    {
        base.Configure(builder);

        builder.ComplexProperty(
            o => o.Name, propertyBuilder =>
            {
                propertyBuilder.Property(n => n.Value)
                    .HasColumnName(propertyBuilder.Metadata.PropertyInfo!.Name)
                    .HasMaxLength(MaxLength.C64).IsRequired();
            });

        builder.Property(a => a.State)
            .HasEnumConvesion()
            .IsRequired();
        builder.Property(a => a.Type)
            .HasEnumConvesion()
            .IsRequired();

        builder.HasOne(d => d.Article)
            .WithMany(a => a.Assets)
            .HasForeignKey(a => a.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.TypeDefinition)
            .WithMany()
            .HasForeignKey(e => e.Type)
            .HasPrincipalKey(e => e.Name)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.ComplexProperty(
            o => o.File, fileBuilder =>
            {
                new FileEntityConfiguration().Configure(fileBuilder);
            });
    }
}