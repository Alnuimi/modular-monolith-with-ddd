using Blocks.Core.Constraints;
using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

internal sealed class PersonEntityConfiguration : EntityConfiguration<Person>
{
    public override void Configure(EntityTypeBuilder<Person> builder)
    {
        // builder.ToTable("Persons");
        
        base.Configure(builder);
        builder.HasIndex(p => p.UserId).IsUnique();
        builder.HasDiscriminator(e => e.TypeDiscriminator)
            .HasValue<Person>(nameof(Person))
            .HasValue<Author>(nameof(Author));
        
        builder.Property(p => p.FirstName).HasMaxLength(MaxLength.C64).IsRequired();
        builder.Property(p => p.LastName).HasMaxLength(MaxLength.C64).IsRequired();
        builder.Property(p => p.Title).HasMaxLength(MaxLength.C64);
        builder.Property(p => p.Affiliation).HasMaxLength(MaxLength.C512).IsRequired()
            .HasComment("Institution or organization they are associated with when they conduct their research.");

        builder.Property(p => p.UserId).IsRequired(false);

        builder.ComplexProperty(
            o => o.EmailAddress, complexBuilder =>
            {
                complexBuilder.Property(p => p.Value)
                    .HasColumnName(complexBuilder.Metadata.PropertyInfo!.Name)
                    .HasMaxLength(MaxLength.C64);
            });

    }
}