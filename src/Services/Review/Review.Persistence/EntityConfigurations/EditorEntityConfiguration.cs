using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Domain.Articles;
using Review.Domain.Reviewers;

namespace Review.Persistence.EntityConfigurations;

internal sealed class EditorEntityConfiguration : IEntityTypeConfiguration<Editor>
{
    public void Configure(EntityTypeBuilder<Editor> builder)
    {
        builder.HasBaseType<Reviewer>();
    }
}