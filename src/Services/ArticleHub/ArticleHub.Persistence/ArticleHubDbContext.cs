
namespace ArticleHub.Persistence;

public class ArticleHubDbContext(DbContextOptions<ArticleHubDbContext> options)
    : DbContext(options)
{
    #region Entities
    public virtual DbSet<Article> Articles { get; set; }
    public virtual DbSet<Journal> Journals { get; set; }
    public virtual DbSet<Person> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSnakeCaseNamingConvention();
         
        base.OnConfiguring(optionsBuilder);
    }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArticleHubDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
