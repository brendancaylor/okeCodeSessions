using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class CollegeAppUser : BaseEntity
    {
        public Guid CollegeId { get; set; }
        public College College { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
