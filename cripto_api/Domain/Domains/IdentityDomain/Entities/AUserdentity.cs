﻿
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domains.IdentityDomain.Entities
{
    public abstract class AUserIdentity : IdentityUser<int>
    {
        public AUserIdentity()
        {
            Roles = new HashSet<RoleIdentity>();
        }

     
        public override string? NormalizedEmail { get => base.NormalizedEmail; set => base.NormalizedEmail = value; }
        public override string? NormalizedUserName { get => base.NormalizedUserName; set => base.NormalizedUserName = value; }
        public virtual string? EmailOptional { get; set; }
        public override string? UserName { get => base.Email; set => base.UserName = base.Email; }
        public override bool TwoFactorEnabled { get => base.TwoFactorEnabled; set => base.TwoFactorEnabled = value; }


        [Required]
        [StringLength(50)]
        public required override string Email { get => base.Email!; set => base.Email = value; }
        public override bool EmailConfirmed { get => base.EmailConfirmed; set => base.EmailConfirmed = value; }

        public override string? PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }
        public override bool PhoneNumberConfirmed { get => base.PhoneNumberConfirmed; set => base.PhoneNumberConfirmed = value; }
        public string Name { get; set; }
        public string? SurName { get; set; }

        public DateTime? CreatedDate { get; set; }
        public ICollection<RoleIdentity> Roles { get; set; }

    }
}
