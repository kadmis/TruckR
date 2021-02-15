namespace Auth.API.Infrastructure.Models.CommandsResults
{
    public interface IResult
    {
        public string Message { get; }
        public bool Successful { get; }
    }
}
