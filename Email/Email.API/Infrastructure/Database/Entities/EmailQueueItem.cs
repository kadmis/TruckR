using BuildingBlocks.Infrastructure.Extensions;
using Email.API.Infrastructure.Database.ValueObjects;
using System;
using System.Text.Json;

namespace Email.API.Infrastructure.Database.Entities
{
    public class EmailQueueItem
    {
        public Guid Id { get; private set; }
        public string Contents { get; private set; }
        public DateTime? SentDate { get; set; }

        private EmailQueueItem()
        {
        }

        public EmailQueueItem(
            string recipientAddress,
            string recipientName,
            string title,
            string message)
        {
            Id = Guid.NewGuid();
            Contents = JsonSerializer.Serialize(new EmailData()
            {
                Address = recipientAddress,
                Name = recipientName,
                Title = title,
                Message = message
            }).ToBase64();
        }

        public EmailData EmailData => JsonSerializer.Deserialize<EmailData>(Contents.FromBase64());
    }
}
