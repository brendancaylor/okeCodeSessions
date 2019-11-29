using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringAngularApps.STS.Models
{
    public class IdentityServiceApiUserTokenForPasswordResetDto
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public string Error { get; set; }
    }
}
