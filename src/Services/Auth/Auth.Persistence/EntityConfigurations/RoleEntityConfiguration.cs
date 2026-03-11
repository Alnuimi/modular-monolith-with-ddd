using Auth.Domain.Roles;
using Blocks.Core.Constraints;
using Blocks.EntityFramework;
using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.EntityConfigurations;

internal sealed class RoleEntityConfiguration : EntityConfiguration<Role>
{
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        base.Configure(builder);
        
        builder.Property(e => e.Type).HasEnumConvesion().IsRequired();
        builder.Property(e => e.Description).IsRequired().HasMaxLength(MaxLength.C256);
    }
}