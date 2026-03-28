using Auth.Domain.Persons;
using Blocks.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Auth.Persistence.Repositories;

public sealed class PersonRepository(AuthDbContext dbContext)
: Repository<AuthDbContext, Person>(dbContext)
{
    public async Task<Person?> GetByUserIdAsync(int userId, CancellationToken ct = default)
    {
        return await Query()
            .SingleOrDefaultAsync(e => e.UserId == userId, ct);
    }
    
    public async Task<Person?> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        return await Query()
            .SingleOrDefaultAsync(e => e.Email.NormalizedEmail == email.ToUpperInvariant(), ct);
    }

    protected override IQueryable<Person> Query()
    {
        return base.Query().Include(p => p.User);
    }
}