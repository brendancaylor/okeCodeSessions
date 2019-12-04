using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore
{
    public class IdentityApiConfirguration : IIdentityApiConfirguration
    {
        public string SpaSpellingClientBaseUrl { get; set; }
        public string SendGridKey { get; set; }
        public string SendGridUser { get; set; }
        public IList<InitialUser> InitialUsers { get; set; } = new List<InitialUser>();
    }

    public class InitialUser
    {
        public string Id { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
