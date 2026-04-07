using Review.Domain.Articles;

namespace Review.Persistence.Repositories;

public sealed class ArticleRepository(ReviewDbContext dbContext)
    : Repository<Article>(dbContext)
{
    
}