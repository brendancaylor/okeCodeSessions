using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Api.Models
{
    public class YearClassDto : BaseDtoFull
    {
        public Guid CollegeId { get; set; }
        public int AcademicYear { get; set; }
        public string YearClassName { get; set; }
        public string TeacherName { get; set; }

        public static YearClassDto From(YearClass yearClass)
        {
            var dto = new YearClassDto();
            dto.Id = yearClass.Id;
            dto.CreatedAt = yearClass.CreatedAt;
            dto.CreatedByAppUserId = yearClass.CreatedByAppUserId;
            dto.RowVersion = yearClass.RowVersion;
            dto.UpdatedAt = yearClass.UpdatedAt;
            dto.UpdatedByAppUserId = yearClass.UpdatedByAppUserId;

            dto.AcademicYear = yearClass.AcademicYear;
            dto.CollegeId = yearClass.CollegeId;
            dto.TeacherName = yearClass.TeacherName;
            dto.YearClassName = yearClass.YearClassName;
            return dto;
        }
    }
}
