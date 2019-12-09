using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Api.Models
{
    public class HomeWorkAssignmentItemBaseDto
    {
        public Guid HomeWorkAssignmentId { get; set; }
        public string Sentence { get; set; }
        public string Word { get; set; }
    }
}
