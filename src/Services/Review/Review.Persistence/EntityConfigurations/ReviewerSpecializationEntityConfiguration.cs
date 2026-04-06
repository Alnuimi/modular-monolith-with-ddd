using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Domain.Articles;

namespace Review.Persistence.EntityConfigurations;

internal sealed class ReviewerSpecializationEntityConfiguration : IEntityTypeConfiguration<ReviewerSpecialization>
{
    public void Configure(EntityTypeBuilder<ReviewerSpecialization> builder)
    {
        builder.HasKey(je => new { je.Journal, je.ReviewerId });

        builder.HasOne(r => r.Journal)
            .WithMany(j => j.Reviewers)
            .HasForeignKey(je => je.JournalId);

        builder.HasOne(r => r.Reviewer)
            .WithMany(j => j.Specializations)
            .HasForeignKey(je => je.ReviewerId);
    }
}