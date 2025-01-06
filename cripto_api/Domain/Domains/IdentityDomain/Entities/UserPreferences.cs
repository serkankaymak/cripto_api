using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domains.IdentityDomain.Entities;

public enum PushNotifcationTopics
{
    notify_general = 1, notify_when_criptos_analysed = 2
}

public class Preference
{
    public Preference(string preferenceType, bool preferenceValue)
    {
        PreferenceType = preferenceType;
        PreferenceValue = preferenceValue;
    }

    public string PreferenceType { get; private set; }
    public bool PreferenceValue { get; private set; }

    public bool IsTrue(PushNotifcationTopics preferenceType)
        => (PreferenceValue && preferenceType.ToString().ToLower().Equals(PreferenceType.Trim().ToLower()));

}



public class UserPreferences : IEntity
{

    public UserPreferences(UserIdentity userIdentity, string preferenceType, bool preferenceValue)
    {
        UserIdentity = userIdentity;
        PreferenceType = preferenceType;
        PreferenceValue = preferenceValue;
    }

    public int Id { get; set; }
    public UserIdentity UserIdentity { get; set; }
    public int UserIdentityId { get; set; }
    public string PreferenceType { get; set; }
    public bool PreferenceValue { get; set; } = true;
    public Preference ToPreference() => new Preference(PreferenceType, PreferenceValue);

}
