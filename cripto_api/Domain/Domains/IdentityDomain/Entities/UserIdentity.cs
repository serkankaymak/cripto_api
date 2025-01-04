using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using static Domain.Domains.IdentityDomain.Entities.RoleIdentity;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.Domains.IdentityDomain.Entities
{
    public class UserIdentity : AUserIdentity
    {
        public UserIdentity()
        {
        }

        public string? PushNotificationToken { get; set; }
        public bool IsBanned { get; set; } = false;
        public string? ProfileImageUrl { get; set; }
        public DateTime? LastActiveDate { get; set; } = DateTime.UtcNow;

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
