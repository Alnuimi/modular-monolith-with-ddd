using Blocks.EntityFramework;
using Review.Domain.Invitations;

namespace Review.Persistence.Repositories;

public sealed class ReviewInvitationRepository(ReviewDbContext dbContext)
    : Repository<ReviewInvitation>(dbContext)
{
    public async Task<ReviewInvitation> GetByTokenOrThrowAsync(string token, CancellationToken cancellationToken)
    {
        return await Entity.SingleOrThrowAsync(x => x.Token == token, cancellationToken);
    }
}
