using Blocks.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Domain.Articles;

namespace Review.Persistence.EntityConfigurations;

internal sealed class ArticleAuthorEntityConfiguration : IEntityTypeConfiguration<ArticleAuthor>
{
    public void Configure(EntityTypeBuilder<ArticleAuthor> builder)
    {
        builder.Property(e => e.ContributionAreas).HasJsonCollectionConversion().IsRequired();
    }
}