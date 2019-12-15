using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Projections
{
    public class CollegeUsage
    {
        public int WordSum { get; set; }
        public int SentenceSum { get; set; }
        public string TeacherName { get; set; }
        public string YearClassName { get; set; }
        public int AcademicYear { get; set; }
        public string CollegeName { get; set; }

        public Guid CollegeId { get; set; }
    }
}
