using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events;

public class UserMobileNotificationTokenUpdatedEvent : IApplicationEvent
{
    public UserMobileNotificationTokenUpdatedEvent(int userId, string mobileNotificationToken, string? userEmail = null)
    {
        UserId = userId;
        MobileNotificationToken = mobileNotificationToken;
        UserEmail = userEmail;
    }

    public DateTime DateOccurred => DateTime.UtcNow;
    public int UserId { get; private set; }
    public string MobileNotificationToken { get; private set; }
    public string? UserEmail { get; private set; }
}
