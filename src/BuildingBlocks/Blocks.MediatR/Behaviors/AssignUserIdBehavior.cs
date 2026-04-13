using Blocks.Core.Security;
using Blocks.Domain;
using MediatR;

namespace Blocks.MediatR.Behaviors;

public class AssignUserIdBehavior<TRequest, TResponse>(IClaimsProvider _claimsProvider)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IAuditableAction
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var userId = _claimsProvider.TryGetUserId();
        if(userId is not null)
        {
            request.CreatedById = userId.Value;
        }
        return await next(cancellationToken);
    }
}