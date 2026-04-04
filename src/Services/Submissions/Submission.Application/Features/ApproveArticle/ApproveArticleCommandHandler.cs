using Auth.Grpc;
using Blocks.EntityFramework;
using Blocks.Exceptions;
using Grpc.Core;
using Journals.Grpc;

namespace Submission.Application.Features.ApproveArticle;

internal sealed class ApproveArticleCommandHandler(
    ArticleRepository _articleRepository,
    PersonRepository _personRepository,
    IPersonService _personClient,
    IJournalService _journalService) : IRequestHandler<ApproveArticleCommand, IdResponse>
{
    public async Task<IdResponse> Handle(ApproveArticleCommand command, CancellationToken ct)
    {
        var article = await _articleRepository.FindByIdOrThrowAsync(command.ArticleId);
        if (!await IsEditorAssignedToJournal(journalId: article.JournalId, userId: command.CreatedById))
        {
            throw new BadRequestException($"Editor is not assigned to the article's Journal ({article.JournalId})");
        }
        
        Person? editor = await GetOrCreatePersonByUserIdAsync(command.CreatedById, ct);
        
        article.Approve(editor);

        await _articleRepository.SaveChangesAsync(ct);
        
        return new IdResponse(article.Id);
    }

    private async Task<bool> IsEditorAssignedToJournal(int journalId, int userId)
    {
        var response = await _journalService.IsEditorAssignedToJournalAsync(
            new IsEditorAssignedToJournalRequest { JournalId = journalId, UserId = userId });
        return response.IsAssigned;
    }
    private async Task<Person> GetOrCreatePersonByUserIdAsync(int userId, CancellationToken ct)
    {
        var person = await _personRepository.GetByUserIdAsync(userId);
        if (person is null)
        {
            var response = await _personClient.GetPersonByUserIdAsync(new GetPersonByUserIdRequest{UserId = userId}, new CallOptions (cancellationToken: ct));
            
            person = Person.Create(response.PersonInfo);
            await _personRepository.AddAsync(person, ct);
        }

        return person;
    }
}