using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Api.Models
{
    public class HomeWorkAssignmentBaseDto
    {
        public Guid YearClassId { get; set; }
        public DateTimeOffset DueDate { get; set; }
    }
}
