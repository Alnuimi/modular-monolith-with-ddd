using Blocks.Core.Constraints;
using Blocks.EntityFramework;
using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

internal sealed class AssetEntityConifguration : EntityConfiguration<Asset>
{
    public override void Configure(EntityTypeBuilder<Asset> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Type)
            .HasEnumConvesion();
        
        builder.ComplexProperty(
            o => o.Name, propertyBuilder =>
            {
                propertyBuilder.Property(n => n.Value)
                    .HasColumnName(propertyBuilder.Metadata.PropertyInfo!.Name)
                    .HasMaxLength(MaxLength.C64).IsRequired();
            });

        builder.ComplexProperty(e => e.File, fileBuilder =>
        {
            new FileEntityConifguration().Configure(fileBuilder);
        });
    }
}