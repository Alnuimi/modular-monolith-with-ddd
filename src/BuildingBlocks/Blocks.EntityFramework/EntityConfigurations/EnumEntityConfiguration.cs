using Blocks.Core.Constraints;
using Blocks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blocks.EntityFramework.EntityConfigurations;

public abstract class EnumEntityConfiguration<T, TEnum> : EntityConfiguration<T, TEnum>
where T : EnumEntity<TEnum>
where TEnum : struct, Enum
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);
        
        builder.HasIndex(b => b.Name).IsUnique();
        builder.Property(b => b.Name)
            .IsRequired()
            .HasEnumConvesion()
            .HasMaxLength(MaxLength.C64)
            .HasColumnOrder(1);
        builder.Property(b => b.Description)
                    .IsRequired()
                    .HasMaxLength(MaxLength.C512)
                    .HasColumnOrder(2);
    }
}