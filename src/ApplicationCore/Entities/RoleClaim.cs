using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class RoleClaim : BaseEntity
    {
        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        public Guid ClaimId { get; set; }
        public Claim Claim { get; set; }
    }
}
