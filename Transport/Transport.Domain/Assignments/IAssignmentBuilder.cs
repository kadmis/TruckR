﻿using System;

namespace Transport.Domain.Assignments
{
    public interface IAssignmentBuilder
    {
        public IAssignmentBuilder AddBasicInformation(string title, string description, DateTime deadline);
        public IAssignmentBuilder AddStartingPoint(string country, string city, string street, string postalCode);
        public IAssignmentBuilder AddDestination(string country, string city, string street, string postalCode);
        public IAssignmentBuilder AddDispatcher(Guid dispatcherId);
        public IAssignmentBuilder AddTransportDocument(string name, long ordinalNumber);
        public Assignment Build();
    }
}
