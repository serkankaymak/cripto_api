﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domains.IdentityDomain.Entities
{
    public class UserLogin : IdentityUserLogin<int>, IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
