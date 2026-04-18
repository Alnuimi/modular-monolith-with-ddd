using Blocks.Core.Constraints;
using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Domain.Shared;

namespace Review.Persistence.EntityConfigurations;

internal sealed  class JournalEntityConfiguration :  EntityConfiguration<Journal>
{
    protected override bool HasGeneratedId => false;

    public override void Configure(EntityTypeBuilder<Journal> builder)
    {
        base.Configure(builder);
        
        builder.Property(j => j.Name)
            .HasMaxLength(MaxLength.C64).IsRequired();
        
        builder.Property(j => j.Abbreviation)
            .HasMaxLength(MaxLength.C16).IsRequired();

         builder.HasMany(r => r.Reviewers).WithOne(j => j.Journal);
    }
}