using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events;

public interface IEventHandler<in T> where T : IEvent
{
    Task HandleAsync(T @event);
}
