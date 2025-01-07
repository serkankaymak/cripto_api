using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events;

public class TickerFetchFailedEvent : AApplicationEvent
{
    public string Message { get; set; }

    public TickerFetchFailedEvent(string message)
    {
        Message = message;
    }

    public string? Url { get; set; }
}

