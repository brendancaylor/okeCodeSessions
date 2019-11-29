using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringAngularApps.STS
{

    public interface IOtherConfirguration
    {
        string Test { get; set; }
        IList<InitialUser> InitialUsers { get; set; }
    }

    public class OtherConfirguration: IOtherConfirguration
    {
        public string Test { get; set; }
        public IList<InitialUser> InitialUsers { get; set; } = new List<InitialUser>();
    }

    public class InitialUser
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
