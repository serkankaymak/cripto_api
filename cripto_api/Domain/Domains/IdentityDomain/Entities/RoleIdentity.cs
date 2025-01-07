using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Domains.IdentityDomain.Entities
{

    public class RoleIdentity : IdentityRole<int>, IEntity
    {
        public enum RolesEnum
        {
            SystemAdministrator, Admin, Member, Tester
        }
        public override string? NormalizedName { get => base.NormalizedName; set => base.NormalizedName = value; }
        public ICollection<UserIdentity> Users { get; set; }
        public RoleIdentity()
        {
            Users = new HashSet<UserIdentity>();
        }
        public RoleIdentity(string roleName)
        {
            Users = new HashSet<UserIdentity>();
            Name = roleName;
            NormalizedName = roleName.ToUpper();
        }
    }
}
