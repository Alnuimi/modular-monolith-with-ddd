using Auth.Domain.Users;
using Auth.Domain.Users.Events;
using Blocks.AspNetCore;
using EmailService.Contracts;
using FastEndpoints;
using Flurl;
using Microsoft.Extensions.Options;

namespace Auth.API.Features.Users.CreateUser;

public class SendConfirmationEmailOnUserCreatedHandler
(IEmailService _emailService,IHttpContextAccessor _httpContextAccessor, IOptions<EmailOptions> _emailOptions)
: IEventHandler<UserCreated>
{
    public async Task HandleAsync(UserCreated eventModel, CancellationToken ct)
    {
        var url = _httpContextAccessor.HttpContext?.Request.BaseUrl()
            .AppendPathSegment("password")
            .SetQueryParams(new {eventModel.ResetPasswordToken});
        
        var emailMessage = BuildConfirmationEmail(eventModel.User, url, _emailOptions.Value.EmailFromAddress);
        await _emailService.SendEmailAsync(emailMessage, ct);
    }

    public EmailMessage BuildConfirmationEmail(User user, string resetLink, string fromEmailAddress)
    {
        const string confirmationEmail =
            "Dear {0},<br>An account has been created for you.<br/>Please set your password using the following URL: <br/>{1}";
        
        return new EmailMessage
        (
            Subject : "Your Account Has Been Created - Set Your Password",
            Content : new Content(ContentType.Html, string.Format(confirmationEmail, user.Person.FullName, resetLink)),
            From : new EmailAddress("Articles", fromEmailAddress),
            To : [new EmailAddress(user.Person.FullName, user.Email!)]
        );
        
    }
}