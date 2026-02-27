using Blocks.Core.Constraints;
using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

internal sealed class JournalEntityConfiguration : EntityConfiguration<Journal>
{
    public override void Configure(EntityTypeBuilder<Journal> builder)
    {
        // builder.ToTable("Journals");
        
        base.Configure(builder);

        builder.Property(j => j.Name)
            .HasMaxLength(MaxLength.C64).IsRequired();

        builder.Property(j => j.Abreviation)
            .HasMaxLength(MaxLength.C8).IsRequired();
    }
}