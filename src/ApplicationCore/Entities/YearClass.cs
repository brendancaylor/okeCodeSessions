using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class YearClass : BaseEntityFull
    {
        public Guid CollegeId { get; set; }
        public College College { get; set; }
        public int AcademicYear { get; set; }
        public string YearClassName { get; set; }
        public string TeacherName { get; set; }

        public string DefaultWordLanguage { get; set; }
        public string DefaultSentenceLanguage { get; set; }

        private readonly List<HomeWorkAssignment> _homeWorkAssignments = new List<HomeWorkAssignment>();
        public IReadOnlyCollection<HomeWorkAssignment> HomeWorkAssignments => _homeWorkAssignments.AsReadOnly();

        public void AddHomeWorkAssignment(HomeWorkAssignment homeWorkAssignment)
        {
            homeWorkAssignment.YearClass = this;
            homeWorkAssignment.YearClassId = this.Id;
            this._homeWorkAssignments.Add(homeWorkAssignment);
        }
    }
}
