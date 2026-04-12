using System.Linq.Expressions;
using Blocks.Core;
using Blocks.Domain.Entities;
using Blocks.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Blocks.EntityFramework;

public static class RepositoryExtensions
{
    public static async Task<TEntity> FindByIdOrThrowAsync<TEntity, TContext>(
        this Repository<TContext, TEntity> repository, int id)
        where TContext : DbContext
        where TEntity : class, IEntity
    {
        var entity = await repository.FindByIdAsync(id);
        if(entity is null)
            throw new NotFoundException($"{typeof(TEntity).Name} not found");
        return entity;
    }
    
    public static async Task<TEntity> FindByIdOrThrowAsync<TEntity>(
        this DbSet<TEntity> dbSet, int id)
        where TEntity : class, IEntity
    {
        var entity = await dbSet.FindAsync(id);
        if(entity is null)
            throw new NotFoundException($"{typeof(TEntity).Name} not found");
        return entity;
    }
    
    public static async Task<TEntity> GetByIdOrThrowAsync<TEntity, TContext>(
        this Repository<TContext, TEntity> repository, int id, CancellationToken cancellationToken = default)
        where TContext : DbContext
        where TEntity : class, IEntity
    {
        var entity = await repository.GetByIdAsync(id);
        if(entity is null)
            throw new NotFoundException($"{typeof(TEntity).Name} not found");
        return entity;
    }

    public static async Task<TEntity> SingleOrThrowAsync<TEntity>(
        this IQueryable<TEntity> source, Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
        where TEntity : class, IEntity<int>
    {
        return Guard.NotFound(await source.SingleOrDefaultAsync(predicate, ct));
    }
}