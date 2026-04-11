using Microsoft.EntityFrameworkCore;
using Review.Domain.Articles;
using Review.Domain.Assets;
using Review.Domain.Invitations;
using Review.Domain.Shared;

namespace Review.Persistence;

public partial class ReviewDbContext(DbContextOptions<ReviewDbContext> options)
    : DbContext(options)
{
    #region Entities 
    // We should only have DbSet for aggregate roots. We can query other entities through the aggregate root's navigation properties.
    // And we going to keep (Reviewers and Editors) because maybe we might need to load some reviewers for instance, when we are inviting the reviewers and the editors as well.

    public virtual DbSet<Article> Articles { get; set; }
    public virtual DbSet<Asset>  Assets { get; set; }
    public virtual DbSet<Journal>  Journals { get; set; }
    public virtual DbSet<Person>  Persons { get; set; }
    public virtual DbSet<Reviewer>  Reviewers { get; set; }
    public virtual DbSet<Editor> Editors { get; set; }
    public virtual DbSet<ReviewInvitation> Invitations { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReviewDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}