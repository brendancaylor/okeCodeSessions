using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class College : BaseEntityFull
    {
        public string CollegeName { get; set; }

        private readonly List<CollegeAppUser> _collegeAppUsers = new List<CollegeAppUser>();
        public IReadOnlyCollection<CollegeAppUser> CollegeAppUsers => _collegeAppUsers.AsReadOnly();

        private readonly List<YearClass> _yearClasses = new List<YearClass>();
        public IReadOnlyCollection<YearClass> YearClasses => _yearClasses.AsReadOnly();

        public void AddCollegeAppUser(CollegeAppUser collegeAppUser)
        {
            collegeAppUser.College = this;
            collegeAppUser.CollegeId = this.Id;
            this._collegeAppUsers.Add(collegeAppUser);
        }

        public void AddYearClass(YearClass yearClass)
        {
            yearClass.College = this;
            yearClass.CollegeId = this.Id;
            this._yearClasses.Add(yearClass);
        }
    }
}
