using System.ComponentModel.DataAnnotations;

namespace Email.API.Infrastructure.Models
{
    public class EmailModel
    {
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z")]
        public string RecipientAddress { get; set; }
        public string RecipientName { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
