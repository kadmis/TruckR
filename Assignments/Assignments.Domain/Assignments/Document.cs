using BuildingBlocks.Domain;
using System;
using System.Threading.Tasks;

namespace Transport.Domain.Assignments
{
    public class Document : IEntity<Guid>
    {
        public Guid Id { get; }
        public string Number { get; }

        private Document()
        {

        }
        private Document(Guid id, string number)
        {
            Id = id;
            Number = number;
        }

        public static Document Create(Guid id, string number)
        {
            return new Document(id, number);
        }
    }
}
