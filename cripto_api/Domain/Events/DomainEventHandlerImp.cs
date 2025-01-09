using Shared.Events;

namespace Domain.Events;

public class DomainEventHandlerImp : IEventHandler<IDomainEvent>
{
    public Task HandleAsync(IDomainEvent @event)
    {
        throw new NotImplementedException();
    }
}
