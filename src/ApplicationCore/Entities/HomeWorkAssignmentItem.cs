using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class HomeWorkAssignmentItem : BaseEntityFull
    {
        public Guid HomeWorkAssignmentId { get; set; }
        public HomeWorkAssignment HomeWorkAssignment { get; set; }

        public string Sentence { get; set; }
        public string Word { get; set; }
        public byte[] SpokenWordAsMp3 { get; set; }
    }
}
