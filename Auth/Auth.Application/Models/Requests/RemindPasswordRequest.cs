namespace Auth.Application.Models.Requests
{
    public class RemindPasswordRequest
    {
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
