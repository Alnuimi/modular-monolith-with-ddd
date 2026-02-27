using Blocks.Core.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

internal sealed class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        // builder.ToTable("Authors");
        
        builder.Property(a => a.Discipline).HasMaxLength(MaxLength.C64)
            .HasComment("The author's main field of study or research (e.g Biology, Computer Science)");

        builder.Property(a => a.Degree).HasMaxLength(MaxLength.C64)
            .HasComment("The author's highest academic qualification (e.g PhD in Mathematics, MSc in Chemistry)");

    }
}