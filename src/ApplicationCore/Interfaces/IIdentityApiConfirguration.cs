using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IIdentityApiConfirguration
    {
        string SpaSpellingClientBaseUrl { get; set; }
        IList<InitialUser> InitialUsers { get; set; }
    }
}
