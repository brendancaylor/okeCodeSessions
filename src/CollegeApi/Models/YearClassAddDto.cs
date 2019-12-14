using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Api.Models
{
    public class YearClassAddDto : YearClassBaseDto
    {
        public static YearClass GetDomainObjectFrom(YearClassAddDto dto)
        {
            var domainObject = new YearClass();
            domainObject.AcademicYear = dto.AcademicYear;
            domainObject.AcademicYear = dto.AcademicYear;
            domainObject.CollegeId = dto.CollegeId;
            domainObject.TeacherName = dto.TeacherName;
            domainObject.YearClassName = dto.YearClassName;
            domainObject.DefaultWordLanguage = dto.DefaultWordLanguage;
            domainObject.DefaultSentenceLanguage = dto.DefaultSentenceLanguage;
            return domainObject;
        }
    }
}
