using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domains.IdentityDomain.Entities;

public enum PreferenceTypes
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
}



public class UserPreferences : IEntity
{
    public int Id { get; set; }
    public required UserIdentity UserIdentity { get; set; }
    public int UserIdentityId { get; set; }

    public required string PreferenceType { get; set; }
    public bool PreferenceValue { get; set; } = true;

    public Preference ToPreference() => new Preference(PreferenceType, PreferenceValue);

}
