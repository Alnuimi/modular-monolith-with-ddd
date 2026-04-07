using Articles.Abstractions.Enums;
using Blocks.EntityFramework;
using Microsoft.Extensions.Caching.Memory;
using Review.Domain.Assets;

namespace Review.Persistence.Repositories;

public sealed class AssetTypeDefinitionRepository(ReviewDbContext dbContext, IMemoryCache cache)
    : CachedRepository<ReviewDbContext, AssetTypeDefinition, AssetType>(dbContext, cache)
{
    
}