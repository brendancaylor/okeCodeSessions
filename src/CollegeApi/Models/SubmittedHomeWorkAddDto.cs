using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Api.Models
{
    public class SubmittedHomeWorkAddDto
    {
        public Guid HomeWorkAssignmentId { get; set; }
        public string StudentName { get; set; }
        public int Score { get; set; }

        public static SubmittedHomeWork GetDomainObjectFrom(SubmittedHomeWorkAddDto dto)
        {
            var domainObject = new SubmittedHomeWork();
            domainObject.HomeWorkAssignmentId = dto.HomeWorkAssignmentId;
            domainObject.Score= dto.Score;
            domainObject.StudentName = dto.StudentName;
            return domainObject;
        }
        
    }
}
