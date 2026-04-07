using Blocks.Domain.Entities;
using Blocks.EntityFramework;
namespace Review.Persistence.Repositories;

public class Repository<TEntity>(ReviewDbContext dbContext) 
    : Repository<ReviewDbContext, TEntity>(dbContext)
    where TEntity : class , IEntity
{
    // todo we rename Repository to RepositoryBase in Blocks.EntityFramework
}