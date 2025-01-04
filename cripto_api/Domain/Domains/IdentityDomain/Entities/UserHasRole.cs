using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domains.IdentityDomain.Entities
{

    public class UserHasRole : IdentityUserRole<int>, IEntity
    {
        [NotMapped]
        public int Id { get; set; }
        public override int UserId { get => base.UserId; set => base.UserId = value; }
        public override int RoleId { get => base.RoleId; set => base.RoleId = value; }
    }
}
