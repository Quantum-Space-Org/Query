using Quantum.Domain.Messages.Event;

namespace Quantum.Query
{
    public interface IAmInterestedIn<ThisEvent>
        where ThisEvent : IsADomainEvent
    {
        Task Subscribe(ThisEvent @event);
    }
}
