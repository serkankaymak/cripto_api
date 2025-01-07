using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Shared.ApiResponse;
using Shared.Extensions;

namespace Domain.Domains.IdentityDomain.Entities;


public enum MobileClientType
{
    Android = 1, Ios = 2
}


public class UserIdentity : AUserIdentity, IEntity, ISoftDeleteable
{
    private ICollection<UserPreferences> _preferences;
    public ICollection<UserPreferences> Preferences
    {
        get
        {
            foreach (PushNotifcationTopics pushNotifcationTopic in EnumExtension.GetValues<PushNotifcationTopics>())
            {
                if (!_preferences.Any(x => x.PreferenceType.Trim().ToLower() == pushNotifcationTopic.ToString().Trim().ToLower()))
                {
                    _preferences.Add(new UserPreferences(this, pushNotifcationTopic.ToString().ToLower().Trim(), true));
                }
            }
            return _preferences;
        }
        set { _preferences = value; }
    }
    public UserIdentity()
    {
        _preferences = new HashSet<UserPreferences>();
    }

    public bool IsNotifyMe(PushNotifcationTopics preferenceType) => Preferences.Select(x => x.ToPreference()).Any(x => x.IsTrue(preferenceType));
    public string? PushNotificationToken { get; set; }
    public MobileClientType? MobileClientType { get; set; }
    [NotMapped]
    public bool IsBanned { get; set; } = false;
    public string? ProfileImageUrl { get; set; }
    public DateTime? LastActiveDate { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; }
    [NotMapped]
    public bool IsActive { get; set; }

    public void UpdateMobileNotificationToken(string token)
    {
        if (token.Length < 10) throw ExceptionFactory.UnprocessableEntity();
        PushNotificationToken = token;
    }

    public string GetMaskedPhone() => PhoneNumber != null ? PhoneNumber.GetMaskedValue() : throw ExceptionFactory.UnprocessableEntity();

    public string GetMaskedEmail()
    {
        int atIndex = Email.IndexOf('@');
        string _name = Email.Substring(0, atIndex);
        string _domain = Email.Substring(atIndex);
        int visibleLength = _name.Length / 2;
        string visiblePart = _name.Substring(0, visibleLength);
        string maskedPart = new string('*', _name.Length - visibleLength);
        return visiblePart + maskedPart + _domain;
    }
}
