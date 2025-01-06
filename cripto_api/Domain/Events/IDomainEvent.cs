using Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events;
public interface IDomainEvent : IEvent { }

public abstract class ADomainEvent : IDomainEvent
{
    public DateTime DateOccurred => DateTime.UtcNow;
}
