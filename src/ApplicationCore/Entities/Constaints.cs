using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Constaints
    {
        //claims
        public const string ClaimAdminisiterClaims = "AdminisiterClaims";
        public const string ClaimAdminisiterRoles = "AdminisiterRoles";
        public const string ClaimAdminisiterAllUsers = "AdminisiterAllUsers";
        public const string ClaimAdminisiterColleges = "AdminisiterColleges";
        public const string ClaimAdminisiterCollegeUsers = "AdminisiterCollegeUsers";
        public const string ClaimAdminisiterHomework = "AdminisiterHomework";

        //roles
        public const string RoleFullAdmin = "FullAdmin";
        public const string RoleCollegeAdmin = "CollegeAdmin";
        public const string RoleTeacher = "Teacher";

    }
}
