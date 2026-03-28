using Auth.Domain.Users;
using Blocks.Core.Constraints;
using Blocks.EntityFramework;
using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.EntityConfigurations;

internal sealed class UserEntityConfiguration : EntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        
        builder.HasMany(e => e.UserRoles)
            .WithOne()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

         builder.HasMany(e => e.RefreshTokens)
            .WithOne()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

         builder.HasOne(u => u.Person)
             .WithOne(p => p.User)
             .HasForeignKey<User>(u => u.PersonId)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);
    }
    
}