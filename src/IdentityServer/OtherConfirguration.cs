using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{

    public interface IOtherConfirguration
    {
        string Test { get; set; }
        string SpaSpellingClientBaseUrl { get; set; }
        IList<InitialUser> InitialUsers { get; set; }
    }

    public class OtherConfirguration: IOtherConfirguration
    {
        public string Test { get; set; }
        public string SpaSpellingClientBaseUrl { get; set; }
        public IList<InitialUser> InitialUsers { get; set; } = new List<InitialUser>();
    }

    public class InitialUser
    {
        public string Id { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
