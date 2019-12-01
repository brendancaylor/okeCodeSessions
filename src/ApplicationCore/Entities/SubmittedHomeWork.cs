using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class SubmittedHomeWork : BaseEntityDateStamps
    {
        public Guid HomeWorkAssignmentId { get; set; }
        public HomeWorkAssignment HomeWorkAssignment { get; set; }
        public string StudentName { get; set; }
        public int Score { get; set; }
    }
}
