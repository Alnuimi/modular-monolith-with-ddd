using Blocks.Core.Constraints;
using Blocks.EntityFramework;
using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Domain.Invitations;

namespace Review.Persistence.EntityConfigurations;

internal sealed class ReviewInvitationEntityConfiguration : EntityConfiguration<ReviewInvitation>
{
    public override void Configure(EntityTypeBuilder<ReviewInvitation> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.UserId).IsRequired(false);
        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(MaxLength.C64);
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(MaxLength.C256);
        builder.Property(e => e.Status).IsRequired().HasEnumConvesion();
    
        builder.ComplexProperty(
            o => o.Email, complexBuilder =>
            {
                complexBuilder.Property(p => p.Value)
                    .HasColumnName(complexBuilder.Metadata.PropertyInfo!.Name)
                    .HasMaxLength(MaxLength.C64);
            });

         builder.ComplexProperty(
            o => o.Token, complexBuilder =>
            {
                complexBuilder.Property(p => p.Value)
                    .HasColumnName(complexBuilder.Metadata.PropertyInfo!.Name)
                    .HasMaxLength(MaxLength.C64);
            });

        builder.HasOne(e => e.SentBy)
            .WithMany()
            .HasForeignKey(e => e.SentById);
        
    }   
}
