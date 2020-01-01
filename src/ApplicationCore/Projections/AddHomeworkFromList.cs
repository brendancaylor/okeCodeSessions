using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Projections
{
    public class AddHomeworkFromList
    {
        public Guid StandardListId { get; set; }
        public Guid YearClassId { get; set; }
        public List<AddHomeworkAssignmentFromList> AddHomeworkAssignments { get; set; }
    }

    public class AddHomeworkAssignmentFromList
    {
        public DateTimeOffset DueDate { get; set; }

        public List<Guid> StandardListItemIds { get; set; } = new List<Guid>();
    }
}
