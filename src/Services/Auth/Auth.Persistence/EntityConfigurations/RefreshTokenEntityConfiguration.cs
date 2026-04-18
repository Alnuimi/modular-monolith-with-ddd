using Auth.Domain.Users;
using Blocks.Core.Constraints;
using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.EntityConfigurations;

internal sealed class RefreshTokenEntityConfiguration : EntityConfiguration<RefreshToken>
{
    public override void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        base.Configure(builder);
        builder.Property(e => e.CreatedByIp).HasMaxLength(MaxLength.C128);
        builder.Property(e => e.RevokedByIp).HasMaxLength(MaxLength.C128);
    }
}
