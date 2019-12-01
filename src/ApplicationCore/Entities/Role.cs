using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }
        public string DisplayName { get; set; }

        private readonly List<RoleClaim> _roleClaims = new List<RoleClaim>();
        public IReadOnlyCollection<RoleClaim> RoleClaims => _roleClaims.AsReadOnly();

        private readonly List<AppUser> _appUsers = new List<AppUser>();
        public IReadOnlyCollection<AppUser> AppUsers => _appUsers.AsReadOnly();

        public void AddRoleClaim(RoleClaim roleClaim)
        {
            roleClaim.Role = this;
            roleClaim.RoleId = this.Id;
            this._roleClaims.Add(roleClaim);
        }

        public void AddAppUser(AppUser appUser)
        {
            appUser.Role = this;
            appUser.RoleId = this.Id;
            this._appUsers.Add(appUser);
        }
    }
}
