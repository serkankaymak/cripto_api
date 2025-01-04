using Shared.Events;
/**
     ASP.NET Core'da, singleton servisler içinde scoped servisleri doğrudan enjekte edemezsiniz.
     Çünkü singleton servisler uygulama ömrü boyunca yaşarlar,
     ancak scoped servisler belirli bir scope (örneğin, bir HTTP isteği) boyunca yaşarlar.
*/

namespace Infastructure.EventBus;
public class InMemoryEventBus : IEventDispatcher
{


    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<Type, List<Type>> _handlers;

    public InMemoryEventBus(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _handlers = new Dictionary<Type, List<Type>>();
    }

    public void Subscribe<TEvent, THandler>()
     where TEvent : IEvent
     where THandler : IEventHandler<TEvent>
    {
        var eventType = typeof(TEvent);
        var handlerType = typeof(THandler);

        if (!_handlers.ContainsKey(eventType))
        {
            _handlers[eventType] = new List<Type>();
        }

        _handlers[eventType].Add(handlerType);
    }


    public async Task Publish<TEvent>(TEvent @event) where TEvent : IEvent
    {
        var eventType = @event.GetType();
        if (_handlers.ContainsKey(eventType))
        {
            var handlerTypes = _handlers[eventType];
            foreach (var handlerType in handlerTypes)
            {
                // Servis sağlayıcıdan handler örneğini alın
                var handler = (IEventHandler<TEvent>)_serviceProvider.GetService(handlerType);
                if (handler != null)
                {
                    // Handle metodunu çağırın ve await edin
                    await handler.Handle(@event);
                }
            }
        }
    }



}
