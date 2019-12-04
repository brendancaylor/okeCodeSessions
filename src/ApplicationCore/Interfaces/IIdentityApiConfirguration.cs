using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IIdentityApiConfirguration
    {
        string SpaSpellingClientBaseUrl { get; set; }
        string SendGridKey { get; set; }
        string SendGridUser { get; set; }
        IList<InitialUser> InitialUsers { get; set; }
    }
}
