using System;
using Microsoft.EntityFrameworkCore;
using Review.Domain.Articles;

namespace Review.Persistence.Repositories;

public sealed class ReivewerRepository(ReviewDbContext dbContext)
    : Repository<Reviewer>(dbContext)
{
    public async Task<Reviewer?> GetByUserIdAsync(int userId, CancellationToken ct = default)
    {
        return await Query().FirstOrDefaultAsync(r => r.UserId == userId, ct);
    }
}
