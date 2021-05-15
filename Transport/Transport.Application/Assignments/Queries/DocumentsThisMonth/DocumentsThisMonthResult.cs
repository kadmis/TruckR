using BuildingBlocks.Application.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Application.Assignments.Queries.DocumentsThisMonth
{
    public class DocumentsThisMonthResult : IResult
    {
        public long? Count { get; private set; }
        public string Message { get; private set; }
        public bool Successful { get; private set; }

        private DocumentsThisMonthResult()
        {
        }

        public static DocumentsThisMonthResult Success(long count)
        {
            return new DocumentsThisMonthResult
            {
                Count = count,
                Successful = true,
                Message = string.Empty
            };
        }

        public static DocumentsThisMonthResult Fail(string message)
        {
            return new DocumentsThisMonthResult
            {
                Successful = false,
                Message = message
            };
        }
    }
}
