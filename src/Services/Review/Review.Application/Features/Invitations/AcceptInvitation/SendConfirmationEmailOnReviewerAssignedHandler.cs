using System;
using EmailService.Contracts;
using MediatR;
using Microsoft.Extensions.Options;
using Review.Domain.Articles.Events;
using Review.Domain.Reviewers;

namespace Review.Application.Features.Invitations.AcceptInvitation;

public sealed class SendConfirmationEmailOnReviewerAssignedHandler(
    IOptions<EmailOptions> _emailOptions,
    IEmailService _emailService)
    : INotificationHandler<ReviewerAssigned>
{
    public async Task Handle(ReviewerAssigned notification, CancellationToken cancellationToken)
    {
        await _emailService.SendEmailAsync(BuildEmailMessage(notification.Article, notification.Reviewer, notification.Article.Editor), cancellationToken);
    }

        private EmailMessage BuildEmailMessage(Article article, Reviewer reviewer, Editor editor)
    {
        const string EmailBody = 
            @"Dear Editor, The reviewer {0} has assigned to  the following article: {1}.
            ";

     

        return new EmailMessage(
            "Reviewer  Assigned",
            new Content(ContentType.Html, string.Format(EmailBody, reviewer.FullName, article.Title)),
            new EmailAddress("articles", _emailOptions.Value.EmailFromAddress),
            new List<EmailAddress> { new EmailAddress(editor.FullName, editor.Email)}
        );
    }
}
