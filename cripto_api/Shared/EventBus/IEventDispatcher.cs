using Shared.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events;

public interface IEventDispatcher
{
    Task Publish<TEvent>(IEventPublisher publisher, TEvent @event)
        where TEvent : IEvent;
    void Subscribe<TEvent, THandler>()
        where TEvent : IEvent
        where THandler : IEventHandler<TEvent>;
}
