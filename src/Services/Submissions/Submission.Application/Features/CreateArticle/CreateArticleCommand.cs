using Blocks.Core.FluentValidation;

namespace Submission.Application.Features.CreateArticle
{
    public record CreateArticleCommand(
        int JournalId,
        string Title,
        string Scope,
        ArticleType Type) : ArticleCommand,IRequest<IdResponse>
    {
        public override ArticleActionType ActionType => ArticleActionType.Create;
    }

    public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
    {
        public CreateArticleCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmptyWithMessage(nameof(CreateArticleCommand.Title));
            RuleFor(x => x.Scope)
                .NotEmptyWithMessage(nameof(CreateArticleCommand.Scope));
            RuleFor(x => x.JournalId)
                .GreaterThan(0).WithMessageForInvalidId(nameof(CreateArticleCommand.JournalId));
        }
    }
}
    