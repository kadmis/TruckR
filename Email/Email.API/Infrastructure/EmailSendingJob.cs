using Email.API.Infrastructure.Database;
using Email.API.Infrastructure.Database.Entities;
using Email.API.Infrastructure.Services;
using Quartz;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Email.API.Infrastructure
{
    [DisallowConcurrentExecution]
    public class EmailSendingJob : IJob
    {
        private readonly IEmailQueueService _queueService;
        private readonly IEmailService _emailService;

        public EmailSendingJob(IEmailService emailService, IEmailQueueService queueService)
        {
            _emailService = emailService;
            _queueService = queueService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var sent = new List<EmailQueueItem>();
            var notYetSent = await _queueService.NotSent(context.CancellationToken);

            foreach (var email in notYetSent)
            {
                var success = await _emailService.SendEmail(email, context.CancellationToken);

                if (success)
                    sent.Add(email);
            }

            await _queueService.SetAsSent(sent, context.CancellationToken);
        }
    }
}
