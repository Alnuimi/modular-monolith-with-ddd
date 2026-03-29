using Microsoft.EntityFrameworkCore;
using Submission.Domain.Entities;

namespace Submission.Persistence.Repositories;

public sealed class PersonRepository(SubmissionDbContext dbContext)
    : Repository<Person>(dbContext)
{
    public async Task<Person?> GetByUserIdAsync(int userId)
    {
        return await Entity.FirstOrDefaultAsync(p => p.UserId == userId);
    }
}