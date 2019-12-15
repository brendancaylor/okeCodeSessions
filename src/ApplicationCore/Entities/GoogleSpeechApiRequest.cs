using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class GoogleSpeechApiRequest : BaseEntity
    {
        public Guid HomeWorkAssignmentItemId { get; set; }
        public HomeWorkAssignmentItem HomeWorkAssignmentItem { get; set; }

        public int SentenceCount { get; set; }
        public int WordCount { get; set; }
    }
}
