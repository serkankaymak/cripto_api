using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events;

public class UserCreatedEvent : ADomainEvent
{
    public UserCreatedEvent(int userId, string? email = null)
    {
        UserId = userId;
        Email = email;
    }

    public int UserId { get; set; }
    public string? Email { get; set; }
}


