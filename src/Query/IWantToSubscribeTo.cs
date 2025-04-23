﻿using System.Collections.Generic;
using System.Linq;
using Quantum.Domain;
using Quantum.Domain.Messages.Event;

namespace Quantum.Query
{
    public abstract class IWantToSubscribeTo<T>
            where T : IsADomainEvent
    {

        private List<IsADomainEvent> Events { get; }
        public IsAnIdentity EventStreamId { get; private set; }
        protected IWantToSubscribeTo()
        {
            Events = new List<IsADomainEvent>();
        }

        protected void Queue(IsAnIdentity eventStreamId, List<IsADomainEvent> domainEvents)
        {
            EventStreamId = eventStreamId;
            Events.AddRange(domainEvents);
        }

        public IsAnIdentity GetEventStreamId()
        {
            return EventStreamId; 
        }
        public bool HasAnyQueuedEvents()
            => EventStreamId != null && Events != null && Events.Any();

        public List<IsADomainEvent> GetQueuedEvents()
        {
            var result = Events.Select(a => a).ToList();
            Events.Clear();
            return result;
        }


        public abstract Task Handle(T @event);
    }
}