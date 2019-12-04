using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Models
{
    public class RequestPasswordResetDto
    {
        [EmailAddress]
        [Required]
        public string Emailaddress { get; set; }
    }
}
