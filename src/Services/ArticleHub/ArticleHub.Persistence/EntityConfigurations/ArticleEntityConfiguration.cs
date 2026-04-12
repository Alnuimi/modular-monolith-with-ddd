using Blocks.Core.Constraints;

namespace ArticleHub.Persistence.EntityConfigurations;

internal sealed class ArticleEntityConfiguration : EntityConfiguration<Article>
{
    public override void Configure(EntityTypeBuilder<Article> builder)
    {
        base.Configure(builder);

        builder.HasIndex(e => e.Title); 

        builder.Property(e => e.Title).HasMaxLength(MaxLength.C256).IsRequired();
        builder.Property(e => e.Dio).HasMaxLength(MaxLength.C64);
        builder.Property(e => e.Stage).HasEnumConvesion().IsRequired();

        builder.Property(e => e.SubmittedOn).HasColumnType("timestamp without time zone").IsRequired();
        builder.Property(e => e.AcceptedOn).HasColumnType("timestamp without time zone");
        builder.Property(e => e.PublishedOn).HasColumnType("timestamp without time zone");

        builder.HasOne(e => e.SubmittedBy).WithMany()
            .HasForeignKey(e => e.SubmittedById)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Journal).WithMany()
            .HasForeignKey(e => e.JournalId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
