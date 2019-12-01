using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Claim : BaseEntity
    {
        public string ClaimName { get; set; }
        public string DisplayName { get; set; }

        private readonly List<RoleClaim> _roleClaims = new List<RoleClaim>();
        public IReadOnlyCollection<RoleClaim> RoleClaims => _roleClaims.AsReadOnly();

        public void AddHoleClaim(RoleClaim holeClaim)
        {
            holeClaim.Claim = this;
            holeClaim.ClaimId = this.Id;
            this._roleClaims.Add(holeClaim);
        }
    }
}
