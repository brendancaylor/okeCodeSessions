﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringAngularApps.STS.Models
{
    public class IdentityServiceApiAddUserDto
    {
        [Required]
        public string Email { get; set; }
    }
}
