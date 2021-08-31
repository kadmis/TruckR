using BuildingBlocks.Application.Models.Results;
using System;
using System.Collections.Generic;

namespace Transport.Application.Assignments.Queries.FreeAssignments
{
    public class FreeAssignmentsResult : IPagedResult<IEnumerable<AssignmentListDTO>>
    {
        public IEnumerable<AssignmentListDTO> Data { get; private set; }
        public int TotalItems { get; private set; }
        public string Message { get; private set; }
        public bool Successful { get; private set; }

        private FreeAssignmentsResult()
        {
        }

        public static FreeAssignmentsResult Success(IEnumerable<AssignmentListDTO> data, int totalItems)
        {
            return new FreeAssignmentsResult
            {
                Data = data,
                TotalItems = totalItems,
                Message = string.Empty,
                Successful = true
            };
        }

        public static FreeAssignmentsResult Fail(string message)
        {
            return new FreeAssignmentsResult
            {
                Message = message,
                Successful = false
            };
        }
    }

    public class AssignmentListDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime CreatedDateUTC { get; set; }
        public DateTime DeadlineUTC { get; set; }
    }
}
