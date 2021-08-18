﻿using MediatR;
using System;

namespace BuildingBlocks.Domain
{
    public interface IDomainEvent : INotification
    {
        Guid Id { get; }
        DateTime OccurredOn { get; }
    }
}
