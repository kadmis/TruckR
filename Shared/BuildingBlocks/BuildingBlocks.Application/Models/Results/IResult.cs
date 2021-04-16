namespace BuildingBlocks.Application.Models.Results
{
    public interface IResult<T> : IResult
    {
        public T Data { get; }
    }

    public interface IResult
    {
        public string Message { get; }
        public bool Successful { get; }
    }
}
