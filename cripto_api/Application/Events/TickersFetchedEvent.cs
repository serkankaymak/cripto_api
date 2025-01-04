using Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events;
public class TickersFetchedEvent : IApplicationEvent
{
    public DateTime DateOccurred { get; set; } = DateTime.UtcNow;


}

