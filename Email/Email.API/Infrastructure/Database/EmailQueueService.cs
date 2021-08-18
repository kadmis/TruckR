using Email.API.Infrastructure.Database.Context;
using Email.API.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Email.API.Infrastructure.Database
{
    public class EmailQueueService : IEmailQueueService
    {
        private readonly EmailContext _context;

        public EmailQueueService(EmailContext context)
        {
            _context = context;
        }

        public async Task Add(EmailQueueItem email, CancellationToken cancellationToken = default)
        {
            _context.EmailQueue.Add(email);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<EmailQueueItem>> NotSent(CancellationToken cancellationToken = default)
        {
            var items = await _context.EmailQueue
                .Where(x => !x.SentDate.HasValue)
                .ToListAsync(cancellationToken);

            return items;
        }

        public async Task SetAsSent(IEnumerable<EmailQueueItem> emails, CancellationToken cancellationToken = default)
        {
            foreach (var email in emails)
                email.SentDate = DateTime.Now;

            _context.EmailQueue.UpdateRange(emails);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
