using BuildingBlocks.EventBus.Externals.Events;
using System;
using System.Reflection;
using System.Text.Json;

namespace BuildingBlocks.EventBus.Externals.EventualConsistency.Database
{
    public class EventEntity
    {
        public Guid Id { get; private set; }
        public string SerializedEvent { get; private set; }
        public string TypeName { get; private set; }
        public string AssemblyName { get; private set; }
        public DateTime OccuredOn { get; private set; }
        public DateTime? ProcessedDate { get; set; }

        private EventEntity()
        {
        }

        public static EventEntity Create<TEvent>(TEvent @event)
            where TEvent : IEvent
        {
            return new EventEntity
            {
                Id = @event.Id,
                OccuredOn = @event.OccuredOn,
                AssemblyName = @event.GetType().Assembly.FullName,
                TypeName = @event.GetType().ToString(),
                SerializedEvent = JsonSerializer.Serialize(@event)
            };
        }

        public dynamic Deserialize()
        {
            var assembly = Assembly.Load(AssemblyName);
            return JsonSerializer.Deserialize(SerializedEvent, assembly.GetType(TypeName));
        }
    }
}
