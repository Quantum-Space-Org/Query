using System.Collections.Generic;
using Quantum.Domain.Messages.Event;

namespace Quantum.Query
{
    public interface IsADomainEventNotifier
    {
        Task Notify(IEnumerable<IsADomainEvent> domainEvents);
        Task Notify<T>(T domainEvents)
            where T : IsADomainEvent;
    }
}
