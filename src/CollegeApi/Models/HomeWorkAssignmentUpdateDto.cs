using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Api.Models
{
    public class HomeWorkAssignmentUpdateDto : HomeWorkAssignmentBaseDto
    {
        public Guid Id { get; set; }
        public byte[] RowVersion { get; set; }


        public static void SetDomainObjectFrom(HomeWorkAssignmentUpdateDto dto, HomeWorkAssignment domainObject)
        {
            domainObject.YearClassId = dto.YearClassId;
            domainObject.DueDate = dto.DueDate;
        }
    }
}
