using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using static Domain.Domains.IdentityDomain.Entities.RoleIdentity;
using static System.Net.Mime.MediaTypeNames;
using Shared.ApiResponse;

namespace Domain.Domains.IdentityDomain.Entities
{
    public class UserIdentity : AUserIdentity, IEntity, ISoftDeleteable
    {


        public ICollection<UserPreferences> Preferences { get; set; }

        public bool isNotifyMeInGeneral()
        {
            return Preferences.Select(x => new Preference(x.PreferenceType, x.PreferenceValue))
                 .Any(x =>
                 x.PreferenceType.Trim().ToLower().Equals(PreferenceTypes.notify_when_criptos_analysed.ToString().Trim().ToLower())
                 &&
                 x.PreferenceValue == true
                 );
        }


        public UserIdentity()
        {
            Preferences = new HashSet<UserPreferences>();
        }

        public string? PushNotificationToken { get; set; }
        public bool IsBanned { get; set; } = false;
        public string? ProfileImageUrl { get; set; }
        public DateTime? LastActiveDate { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; }



        public void UpdateMobileNotificationToken(string token)
        {
            if (token.Length < 10) throw ExceptionFactory.UnprocessableEntity();
            PushNotificationToken = token;
        }

        public string getMaskedEmail()
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
}
