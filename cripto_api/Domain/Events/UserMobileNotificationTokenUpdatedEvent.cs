using Domain.Domains.IdentityDomain.Entities;
using Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events;


public class UserMobileNotificationTokenUpdatedEvent : ADomainEvent
{
    public UserMobileNotificationTokenUpdatedEvent(int userId, ICollection<UserPreferences> preferences,
        string? mobileNotificationTokenOld,
        string mobileNotificationTokenNew,
        string? userEmail = null,
        MobileClientType? mobileClientType = null)
    {
        UserId = userId;
        MobileNotificationTokenOld = mobileNotificationTokenOld;
        MobileNotificationTokenNew = mobileNotificationTokenNew;
        UserEmail = userEmail;
        MobileClientType = mobileClientType;
        Preferences = preferences;
    }
    public int UserId { get; private set; }
    public MobileClientType? MobileClientType { get; private set; }
    public string? MobileNotificationTokenOld { get; private set; }
    public string MobileNotificationTokenNew { get; private set; }
    public string? UserEmail { get; private set; }
    public ICollection<UserPreferences> Preferences { get; private set; }

}
