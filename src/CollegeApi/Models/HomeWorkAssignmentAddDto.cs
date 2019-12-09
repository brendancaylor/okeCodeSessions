using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Api.Models
{
    public class HomeWorkAssignmentAddDto : HomeWorkAssignmentBaseDto
    {
        public static HomeWorkAssignment GetDomainObjectFrom(HomeWorkAssignmentAddDto dto)
        {
            var domainObject = new HomeWorkAssignment();
            domainObject.YearClassId = dto.YearClassId;
            domainObject.DueDate = dto.DueDate;
            return domainObject;
        }
    }
}
