using Blocks.Core.Constraints;
using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Domain.Articles;
using Review.Domain.Reviewers;
using Review.Domain.Shared;

namespace Review.Persistence.EntityConfigurations;

internal sealed class PersonEntityConfiguration :  EntityConfiguration<Person>
{
    protected override bool HasGeneratedId => false;

    public override void Configure(EntityTypeBuilder<Person> builder)
    {
        base.Configure(builder);
        builder.HasIndex(p => p.UserId).IsUnique();

        // using Raw SQL here because at this moment we cannot use a value object to create a composite index
        builder.HasAnnotation(
            "RawSql:Index",
            "CREATE UNIQUE INDEX IX_Person_Email_TypeDiscriminator ON Person (Email, TypeDiscriminator)");

        // talk about EF Core inheritance
        builder.HasDiscriminator(p => p.TypeDiscriminator)
            .HasValue<Person>(nameof(Person))
            .HasValue<Author>(nameof(Author))
            .HasValue<Reviewer>(nameof(Reviewer))
            .HasValue<Editor>(nameof(Editor));

        builder.Property(p => p.UserId).IsRequired(false);
        builder.Property(p => p.FirstName).HasMaxLength(MaxLength.C64).IsRequired();
        builder.Property(p => p.LastName).HasMaxLength(MaxLength.C64).IsRequired();
        builder.Property(p => p.Honorific).HasMaxLength(MaxLength.C32);
        
        builder.Property(p => p.Affiliation).HasMaxLength(MaxLength.C512).IsRequired()
            .HasComment("Institution or organization they are associated with when they conduct their research.");
        
        builder.ComplexProperty(
            o => o.Email, complexBuilder =>
            {
                complexBuilder.Property(p => p.Value)
                    .HasColumnName(complexBuilder.Metadata.PropertyInfo!.Name)
                    .HasMaxLength(MaxLength.C64);
            });
    }
}