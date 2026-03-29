namespace Submission.Application.Features.ApproveArticle;

public sealed record ApproveArticleCommand : ArticleCommand
{
    public override ArticleActionType ActionType => ArticleActionType.Approve;
};

public class ApproveArticleCommandValidator : ArticleCommandValidator<ApproveArticleCommand>;