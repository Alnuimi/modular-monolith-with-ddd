using System;
using FluentValidation;
using Review.Application.Features.Shared;
using Review.Domain.Shared.Enums;

namespace Review.Application.Features.Invitations.AcceptInvitation;

public sealed record AcceptInvitationCommand(string Token)
    : ArticleCommand<ArticleActionType, AcceptInvitationResponse>
{
    public override ArticleActionType ActionType => ArticleActionType.AcceptInvitation;
}

public sealed record AcceptInvitationResponse(int ArticleId, int InvitationId, int ReviewerId);

public class AcceptInvitationCommandValidator : ArticleCommandValidator<AcceptInvitationCommand>
{
    public AcceptInvitationCommandValidator()
    {
        RuleFor(x => x.Token).NotEmpty();
    }
}