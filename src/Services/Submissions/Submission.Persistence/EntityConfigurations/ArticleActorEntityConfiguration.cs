using Articles.Abstractions.Enums;
using Blocks.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

internal sealed class ArticleActorEntityConfiguration : IEntityTypeConfiguration<ArticleActor>
{
    public void Configure(EntityTypeBuilder<ArticleActor> builder)
    {
        // builder.ToTable("ArticleActors");

        builder.HasKey(p => new { p.ArticleId, p.PersonId, p.Role });
        
        builder.HasDiscriminator(p => p.TypeDiscriminator)
            .HasValue<ArticleActor>(nameof(ArticleActor))
            .HasValue<ArticleAuthor>(nameof(ArticleAuthor));
        
        builder.Property(p => p.Role)
            .HasEnumConvesion().HasDefaultValue(UserRoleType.AUT);

        builder.HasOne(aa => aa.Article)
            .WithMany(a => a.Actors)
            .HasForeignKey(aa => aa.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(aa => aa.Person)
            .WithMany(a => a.Actors)
            .HasForeignKey(aa => aa.PersonId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}