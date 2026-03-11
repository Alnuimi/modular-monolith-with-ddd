using EmailService.Contracts;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace EmailService.Smtp;

public sealed class SmtpEmailService(IOptions<EmailOptions> emailOptions) : IEmailService
{
    private readonly EmailOptions _emailOptions = emailOptions.Value;

    public async Task<bool> SendEmailAsync(EmailMessage emailMessage,  CancellationToken cancellationToken)
    {
        var message = emailMessage.ToMailKitMessage();

        using var smtpClient = new SmtpClient();

        try
        {
            await smtpClient.ConnectAsync(
                _emailOptions.Smtp.Host,
                _emailOptions.Smtp.Port,
                _emailOptions.Smtp.UseSSL,
                cancellationToken);
            await smtpClient.AuthenticateAsync(
                _emailOptions.Smtp.Username,
                _emailOptions.Smtp.Password,
                cancellationToken);
            await smtpClient.SendAsync(message, cancellationToken);
           
        }
        catch (Exception e)
        {
            // todo - Logging and throw
            return false;
        }

        return true;
    }
}
