
using Blocks.Core.Security;
using Blocks.Domain;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Blocks.AspNetCore.Grpc;

public sealed class AssignUserIdInterceptor(IClaimsProvider _claimsProvider) : Interceptor
{
    public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>
        (TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
    {
        if(request is IAuditableAction action)
        {
            var userId = _claimsProvider.TryGetUserId();
            if(userId is not null)
            {
                action.CreatedById = userId.Value;
            }
        }
        return base.UnaryServerHandler(request, context, continuation);
    }
}
