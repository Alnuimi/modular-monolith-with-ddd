using Auth.Domain.Persons;
using Blocks.Core.Constraints;
using Blocks.EntityFramework;
using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Persistence.EntityConfigurations;

internal sealed class PersonEntityConfiguration : EntityConfiguration<Person>
{
    public override void Configure(EntityTypeBuilder<Person> builder)
    {
        base.Configure(builder);
        
        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(MaxLength.C64);

        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(MaxLength.C64);

        builder.Property(e => e.Gender)
            .IsRequired()
            .HasEnumConvesion();

        builder.OwnsOne(
            e => e.Honorific, b =>
            {
                b.Property(e => e.Value).HasMaxLength(MaxLength.C32).HasColumnNameSameAsProperty();

                b.WithOwner(); // required to avoid navigation issues
            });

        builder.OwnsOne(
            e => e.ProfessionalProfile, b =>
            {
                b.Property(e => e.Position).HasMaxLength(MaxLength.C32).HasColumnNameSameAsProperty();
                b.Property(e => e.CompanyName).HasMaxLength(MaxLength.C32).HasColumnNameSameAsProperty();
                b.Property(e => e.Affiliation).HasMaxLength(MaxLength.C32).HasColumnNameSameAsProperty();

                b.WithOwner(); // required to avoid navigation issues
            });

        builder.Property(e => e.PictureUrl).HasMaxLength(MaxLength.C2048);
    }
}