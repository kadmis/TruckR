using BuildingBlocks.Application.Models.Results;

namespace Transport.Application.Assignments.Queries.TransportDocumentInfo
{
    public class TransportDocumentInfoResult : IResult<TransportDocumentInfoDTO>
    {
        public TransportDocumentInfoDTO Data { get; private set; }
        public string Message { get; private set; }
        public bool Successful { get; private set; }

        private TransportDocumentInfoResult()
        {
        }

        public static TransportDocumentInfoResult Success(TransportDocumentInfoDTO data)
        {
            return new TransportDocumentInfoResult
            {
                Successful = true,
                Message = string.Empty,
                Data = data
            };
        }

        public static TransportDocumentInfoResult Fail(string message)
        {
            return new TransportDocumentInfoResult
            {
                Successful = false,
                Message = message,
            };
        }
    }

    public class TransportDocumentInfoDTO
    {
        public string Name { get; set; }
        public string Number { get; set; }
    }
}
