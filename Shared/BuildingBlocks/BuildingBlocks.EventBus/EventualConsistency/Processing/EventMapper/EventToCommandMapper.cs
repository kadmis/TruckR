using BuildingBlocks.Application.Commands;
using BuildingBlocks.EventBus.Externals.Events;
using System;
using System.Collections.Concurrent;

namespace BuildingBlocks.EventBus.EventualConsistency.Processing.EventMapper
{
    public class EventToCommandMapper
    {
        private readonly ConcurrentDictionary<Type, IEventToCommandMap> MappingActions;

        public EventToCommandMapper()
        {
            MappingActions = new ConcurrentDictionary<Type, IEventToCommandMap>();
        }

        public EventToCommandMapper Register<TEvent>(IEventToCommandMap mappingFunc)
            where TEvent : IEvent
        {
            MappingActions.TryAdd(typeof(TEvent), mappingFunc);

            return this;
        }

        public ICommand RetrieveCommand(IEvent @event)
        {
            if (MappingActions.TryGetValue(@event.GetType(), out IEventToCommandMap map))
                return map.Map(@event);

            return null;
        }
    }
}
