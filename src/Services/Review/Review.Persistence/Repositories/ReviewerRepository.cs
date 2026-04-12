using System;
using Microsoft.EntityFrameworkCore;
using Review.Domain.Reviewers;

namespace Review.Persistence.Repositories;

public sealed class ReviewerRepository(ReviewDbContext dbContext)
    : Repository<Reviewer>(dbContext)
{
    public async Task<Reviewer?> GetByUserIdAsync(int userId, CancellationToken ct = default)
    {
        return await Entity.FirstOrDefaultAsync(r => r.UserId == userId, ct);
    }

    public async Task<Reviewer?> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        return await Entity.SingleOrDefaultAsync(r => r.Email.Value.ToLower() == email.ToLower(), ct);
    }
}
