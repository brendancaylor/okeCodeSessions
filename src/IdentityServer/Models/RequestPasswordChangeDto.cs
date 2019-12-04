using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Models
{
    public class RequestPasswordChangeDto
    {
        [EmailAddress]
        [DisplayName("Email Address")]
        [Required]
        public string Emailaddress { get; set; }

        //Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character:
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Your password is not strong enough.")]
        [DisplayName("New Password")]
        [Required]
        public string NewPassword { get; set; }
        public string Token { get; set; }

        public IdentityResult IdentityResult { get; set; }
    }
}
