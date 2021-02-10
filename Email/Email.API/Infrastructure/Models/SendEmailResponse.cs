using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Email.API.Infrastructure.Models
{
    public class SendEmailResponse
    {
        public string Message { get; }
        public bool IsSuccess { get; }

        private SendEmailResponse(string message, bool isSuccess)
        {
            Message = message;
            IsSuccess = isSuccess;
        }

        public static SendEmailResponse Success()
        {
            return new SendEmailResponse("Successfully sent message", true);
        }

        public static SendEmailResponse Fail(string message)
        {
            return new SendEmailResponse($"Sending message failed: {message}", false);
        }
    }
}
