using Microsoft.EntityFrameworkCore;
using Review.Domain.Articles;
using Review.Domain.Assets;
using Review.Domain.Shared;

namespace Review.Persistence;

public partial class ReviewDbContext(DbContextOptions<ReviewDbContext> options)
    : DbContext(options)
{
    #region Entities

    public virtual DbSet<Article> Articles { get; set; }
    public virtual DbSet<ArticleActor> ArticleActors { get; set; }
    public virtual DbSet<Asset>  Assets { get; set; }
    public virtual DbSet<AssetTypeDefinition>  AssetTypes { get; set; }
    public virtual DbSet<Journal>  Journals { get; set; }
    public virtual DbSet<Person>  Persons { get; set; }
    public virtual DbSet<Author>  Authors { get; set; }
    public virtual DbSet<Reviewer>  Reviewers { get; set; }
    public virtual DbSet<Editor> Editors { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReviewDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}