using BuildingBlocks.Domain;
using System;
using System.Text;

namespace Transport.Domain.Documents
{
    public class Document : IEntity<Guid>
    {
        public Guid Id { get; private set; }

        public Guid AssignmentId { get; private set; }

        private string _number;

        private string _name;

        private Document()
        {
        }

        internal static Document Create(string name, long ordinalNumber)
        {
            return new Document
            {
                Id = Guid.NewGuid(),
                _number = GenerateNumber(ordinalNumber),
                _name = name,
            };
        }

        private static string GenerateNumber(long ordinalNumber)
        {
            var now = Clock.Now;
            var month = now.ToString("MM");
            var year = now.ToString("yyyy");

            return new StringBuilder()
                .Append("TRANS/")
                .Append(ordinalNumber)
                .Append('/')
                .Append(month)
                .Append('/')
                .Append(year)
                .ToString();
        }
    }
}
