using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Domain.Articles;

namespace Review.Persistence.EntityConfigurations;

internal sealed class EditorEntityConfiguration : IEntityTypeConfiguration<Editor>
{
    public void Configure(EntityTypeBuilder<Editor> builder)
    {
        builder.HasBaseType<Reviewer>();
    }
}