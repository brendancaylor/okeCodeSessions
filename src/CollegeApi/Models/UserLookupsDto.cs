using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Api.Models
{
    public class UserLookupsDto
    {
        public List<LookupDto> Roles { get; set; } = new List<LookupDto>();
        public List<LookupDto> Colleges { get; set; } = new List<LookupDto>();
    }

    public class LookupDto : BaseDto
    {
        public string DisplayName { get; set; }
    }

}
