using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class AppUser : BaseEntityDateStamps
    {
        public string IdentityId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        readonly List<CollegeAppUser> _collegeAppUsers = new List<CollegeAppUser>();
        public IReadOnlyCollection<CollegeAppUser> CollegeAppUsers => _collegeAppUsers.AsReadOnly();

        public void AddCollegeAppUsers(List<Guid> collegeIds)
        {
            this._collegeAppUsers.Clear();
            foreach (var collegeId in collegeIds)
            {
                var collegeAppUser = new CollegeAppUser();
                collegeAppUser.AppUser = this;
                collegeAppUser.CollegeId = collegeId;
                this._collegeAppUsers.Add(collegeAppUser);
            }
        }
    }
}
