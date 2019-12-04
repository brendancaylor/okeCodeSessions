using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore
{
    public class CollegeApiConfirguration : ICollegeApiConfirguration
    {
        public string SpaSpellingClientBaseUrl { get; set; }
        public string IdentityBaseUrl { get; set; }

    }
}
