using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Api
{
    public interface IOtherConfirguration
    {
        string SpaSpellingClientBaseUrl { get; set; }
        string IdentityBaseUrl { get; set; }
    }

    public class OtherConfirguration : IOtherConfirguration
    {
        public string SpaSpellingClientBaseUrl { get; set; }
        public string IdentityBaseUrl { get; set; }
    }
}
