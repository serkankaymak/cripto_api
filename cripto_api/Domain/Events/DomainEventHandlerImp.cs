using Shared.Events;

namespace Domain.Events;

public class DomainEventHandlerImp : IEventHandler<IDomainEvent>
{
    public Task Handle(IDomainEvent @event)
    {
        throw new NotImplementedException();
    }
}
