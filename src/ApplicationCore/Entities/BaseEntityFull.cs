using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public abstract class BaseEntityFull : BaseEntityVersion
    {
        public Guid CreatedByAppUserId { get; set; }

        public AppUser CreatedByAppUser { get; set; }

        public Guid? UpdatedByAppUserId { get; set; }

        public AppUser UpdatedByAppUser { get; set; }
    }
}
