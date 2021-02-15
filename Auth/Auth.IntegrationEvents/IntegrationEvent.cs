﻿using SharedRabbitMQ.Externals.Events;
using System;

namespace Auth.IntegrationEvents
{
    public abstract class IntegrationEvent : IEvent
    {
        public DateTime OccuredOn { get; protected set; }
    }
}