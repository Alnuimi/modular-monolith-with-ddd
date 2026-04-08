using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Domain.Articles;
using Review.Domain.Shared;

namespace Review.Persistence.EntityConfigurations;

internal sealed class ReviewerEntityConfiguration :  IEntityTypeConfiguration<Reviewer>
{
    public void Configure(EntityTypeBuilder<Reviewer> builder)
    {
        builder.HasBaseType<Person>();

        builder.HasMany(r => r.Specializations)
            .WithOne(j => j.Reviewer);
    }
}