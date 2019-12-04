using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface ICollegeApiConfirguration
    {
        string SpaSpellingClientBaseUrl { get; set; }
        string IdentityBaseUrl { get; set; }

    }
}
