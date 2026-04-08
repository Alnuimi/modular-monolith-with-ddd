using Articles.Abstractions.Enums;
using Blocks.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Domain.Articles;

namespace Review.Persistence.EntityConfigurations;

internal sealed class ArticleActorEntityConfiguration : IEntityTypeConfiguration<ArticleActor>
{
    public void Configure(EntityTypeBuilder<ArticleActor> builder)
    {
        builder.HasKey(a => new { a.ArticleId, a.PersonId, a.Role });
        builder.Property(a => a.Role)
            .HasEnumConvesion()
            .HasDefaultValue(UserRoleType.AUT);

        builder.HasDiscriminator(a => a.TypeDiscriminator)
            .HasValue<ArticleActor>(nameof(ArticleActor))
            .HasValue<ArticleAuthor>(nameof(ArticleAuthor));

        builder.HasOne(aa => aa.Article)
            .WithMany(a => a.Actors)
            .HasForeignKey(aa => aa.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(aa => aa.Person)
            .WithMany()
            .HasForeignKey(aa => aa.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}