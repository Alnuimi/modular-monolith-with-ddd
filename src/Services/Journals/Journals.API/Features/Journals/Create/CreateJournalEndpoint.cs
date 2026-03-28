using Articles.Abstractions;
using Articles.Abstractions.Enums;
using Auth.Grpc;
using Blocks.Exceptions;
using Blocks.Redis;
using FastEndpoints;
using Grpc.Core;
using Articles.Security;
using Journals.Domain.Journals;
using Journals.Domain.Journals.Events;
using Mapster;
using Microsoft.AspNetCore.Authorization;
namespace Journals.API.Features.Journals.Create;

[Authorize(Roles = Role.EOF)]
[HttpPost("journals")]
[Tags("Journals")]
internal sealed class CreateJournalEndpoint(
    Repository<Journal> _journalRepository,
    Repository<Editor> _editorRepository,
    IPersonService _personService) : Endpoint<CreateJournalCommand, IdResponse>
{
    public  override async Task HandleAsync(CreateJournalCommand command, CancellationToken ct)
    {
        if(_journalRepository.Collection.Any(j => j.Abbreviation == command.Abbreviation || j.Name == command.Name))
        {
            throw new BadRequestException("A journal with the same name or abbreviation already exists.");
        }

        if(!_editorRepository.Collection.Any(e => e.Id == command.ChiefEditorId))
        {
            // todo - get the editor from Auth service 
            await CreateEditor(command.ChiefEditorId, ct);
        }

        var journal = command.Adapt<Journal>();

        await _journalRepository.AddAsync(journal);
        await _journalRepository.SaveAllAsync();

        // todo - publish JournalCreated event
        await PublishAsync(new JournalCreated(journal));
        
        await SendAsync(new IdResponse(journal.Id), cancellation: ct);
    }

    private async Task CreateEditor(int userId, CancellationToken ct)
    {
        var response = await _personService.GetPersonByUserIdAsync(
            new GetPersonByUserIdRequest {UserId = userId}
            , new CallOptions(cancellationToken: ct));

        var editor = new Editor
        {
            Id = userId,
            PersonId = response.PersonInfo.Id,
            Affiliation = response.PersonInfo.ProfessionalProfile!.Affiliation,
            FullName = response.PersonInfo.FirstName + " " + response.PersonInfo.LastName,
        };

        await _editorRepository.AddAsync(editor);
    }
}
