using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Api.Models
{
    public class YearClassUpdateDto : YearClassBaseDto
    {
        public Guid Id { get; set; }
        public byte[] RowVersion { get; set; }


        public static void SetDomainObjectFrom(YearClassUpdateDto dto, YearClass domainObject)
        {
            domainObject.AcademicYear = dto.AcademicYear;
            domainObject.AcademicYear = dto.AcademicYear;
            domainObject.CollegeId = dto.CollegeId;
            domainObject.TeacherName = dto.TeacherName;
            domainObject.YearClassName = dto.YearClassName;
        }
    }
}
