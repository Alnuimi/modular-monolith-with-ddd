using Blocks.Core.Security;
using Blocks.Domain;
using FastEndpoints;

namespace Blocks.FastEndpoints;

public sealed class AssignUserIdPreProcessor : IGlobalPreProcessor
{
    public Task PreProcessAsync(IPreProcessorContext context, CancellationToken ct)
    {
          if(context.Request is IAuditableAction action)
        {
            var claimProvider = context.HttpContext.Resolve<IClaimsProvider>();
            action.CreatedById = claimProvider.GetUserId();
        }

        return Task.CompletedTask;
    }
}
