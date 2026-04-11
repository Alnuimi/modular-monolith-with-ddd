using Blocks.Core.Constraints;
using Blocks.EntityFramework;
using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Domain.Articles;

namespace Review.Persistence.EntityConfigurations;

internal sealed class ArticleEntityConfiguration : EntityConfiguration<Article>
{
    public override void Configure(EntityTypeBuilder<Article> builder)
    {
        base.Configure(builder);
         
                builder.Property(a => a.Title)
                    .HasMaxLength(MaxLength.C256).IsRequired();
                
                builder.Property(a => a.Scope)
                    .HasMaxLength(MaxLength.C2048).IsRequired();
        
                builder.Property(a => a.Stage)
                    .HasEnumConvesion();
        
                builder.Property(a => a.Type)
                    .HasEnumConvesion();
                
                builder.HasOne(a => a.Journal).WithMany(e => e.Articles)
                    .HasForeignKey(e => e.JournalId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                builder.HasMany(a => a.Assets)
                    .WithOne(a => a.Article)
                    .HasForeignKey(e => e.ArticleId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasMany(a => a.Actors)
                    .WithOne(a => a.Article)
                    .HasForeignKey(e => e.ArticleId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasMany(a => a.Invitations)
                    .WithOne()
                    .HasForeignKey(e => e.ArticleId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(a => a.SubmittedBy).WithMany()
                    .HasForeignKey(a => a.SubmittedById)
                    .HasPrincipalKey(a => a.Id)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
    }
}