using Email.API.Infrastructure.Configuration;
using Email.API.Infrastructure.Models;
using MailKit.Net.Smtp;
using MimeKit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Email.API.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _configuration;

        public EmailService(EmailConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(EmailModel email, CancellationToken cancellationToken = default)
        {
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync(_configuration.Server, _configuration.Port, true, cancellationToken);
                await client.AuthenticateAsync(_configuration.Email, _configuration.Password, cancellationToken);
                await client.SendAsync(CreateMessage(email), cancellationToken);
                await client.DisconnectAsync(true, cancellationToken);
            }
        }

        private MimeMessage CreateMessage(EmailModel email)
        {
            return new MimeMessage
                (
                    new List<MailboxAddress> { new MailboxAddress(_configuration.Name, _configuration.Email) },
                    new List<MailboxAddress> { new MailboxAddress(email.RecipientName, email.RecipientAddress) },
                    email.Title,
                    new TextPart(MimeKit.Text.TextFormat.Html) { Text = email.Message }
                );
        }
    }
}
