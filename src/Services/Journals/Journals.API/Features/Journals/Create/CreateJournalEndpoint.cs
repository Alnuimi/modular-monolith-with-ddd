using Articles.Abstractions;
using Articles.Abstractions.Enums;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
namespace Journals.API.Features.Journals.Create;

[Authorize(Roles = Role.EOF)]
[HttpPost("journals")]
[Tags("Journals")]
public class CreateJournalEndpoint : Endpoint<CreateJournalCommand, IdResponse>
{
    public override Task HandleAsync(CreateJournalCommand req, CancellationToken ct)
    {
        return base.HandleAsync(req, ct);
    }
}
