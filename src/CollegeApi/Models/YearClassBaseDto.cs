using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Api.Models
{
    public class YearClassBaseDto
    {
        public Guid CollegeId { get; set; }
        public int AcademicYear { get; set; }
        public string YearClassName { get; set; }
        public string TeacherName { get; set; }
        public string DefaultWordLanguage { get; set; }
        public string DefaultSentenceLanguage { get; set; }
    }
}
