using Email.API.Infrastructure.Configuration;
using Email.API.Infrastructure.Database.Entities;
using MailKit.Net.Smtp;
using MimeKit;
using System;
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

        public async Task<bool> SendEmail(EmailQueueItem email, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    await client.ConnectAsync(_configuration.Server, _configuration.Port, true, cancellationToken);
                    await client.AuthenticateAsync(_configuration.Email, _configuration.Password, cancellationToken);
                    await client.SendAsync(CreateMessage(email), cancellationToken);
                    await client.DisconnectAsync(true, cancellationToken);
                }

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        private MimeMessage CreateMessage(EmailQueueItem email)
        {
            var emailData = email.EmailData;
            return new MimeMessage
                (
                    new List<MailboxAddress> { new MailboxAddress(_configuration.Name, _configuration.Email) },
                    new List<MailboxAddress> { new MailboxAddress(emailData.Name, emailData.Address) },
                    emailData.Title,
                    new TextPart(MimeKit.Text.TextFormat.Html) { Text = emailData.Message }
                );
        }
    }
}
