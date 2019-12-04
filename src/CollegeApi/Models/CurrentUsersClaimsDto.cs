using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Api.Models
{
    public class CurrentUsersClaimsDto
    {
        public List<string> Claims { get; set; } = new List<string>();
    }
}
