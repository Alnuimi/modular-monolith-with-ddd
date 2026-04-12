using System;
using Blocks.Core.Constraints;
using Blocks.Core.FluentValidation;
using FluentValidation;
using Review.Application.Features.Shared;
using Review.Domain.Shared.Enums;

namespace Review.Application.Features.Invitations.InviteReviewer;

public sealed record InviteReviewerCommand(int? UserId, string Email, string FirstName, string LastName)
    : ArticleCommand<ArticleActionType, InviteReviewerResponse>
{
    public override ArticleActionType ActionType => ArticleActionType.InviteReviewer;
}

public sealed record InviteReviewerResponse(int ArticleId, int InvitationId, string Token);

public sealed class InviteReviewerCommandValidator : ArticleCommandValidator<InviteReviewerCommand>
{
    public InviteReviewerCommandValidator()
    {
        When(c => c.UserId is null , () =>
        {
            RuleFor(c => c.Email)
                .NotEmptyWithMessage(nameof(InviteReviewerCommand.Email))
                .MaximumLengthWithMessage(MaxLength.C64, nameof(InviteReviewerCommand.Email))
                .EmailAddress();

            RuleFor(c => c.FirstName)
                .NotEmptyWithMessage(nameof(InviteReviewerCommand.FirstName))
                .MaximumLengthWithMessage(MaxLength.C64, nameof(InviteReviewerCommand.FirstName));

            RuleFor(c => c.LastName)
                .NotEmptyWithMessage(nameof(InviteReviewerCommand.LastName))
                .MaximumLengthWithMessage(MaxLength.C256, nameof(InviteReviewerCommand.LastName));
        });
    }
}