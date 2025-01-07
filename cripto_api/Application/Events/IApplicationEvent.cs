using Shared.Events;

namespace Application.Events;
public interface IApplicationEvent : IEvent
{
}

public abstract class AApplicationEvent : IApplicationEvent
{
    public DateTime DateOccurred => DateTime.UtcNow;
}

