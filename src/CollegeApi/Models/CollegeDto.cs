using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace College.Api.Models
{
    public class CollegeDto : BaseDtoFull
    {
        public string CollegeName { get; set; }

        public static CollegeDto From(ApplicationCore.Entities.College college)
        {
            var dto = new CollegeDto();
            dto.Id = college.Id;
            dto.CollegeName = college.CollegeName;
            dto.CreatedAt = college.CreatedAt;
            dto.CreatedByAppUserId = college.CreatedByAppUserId;
            dto.RowVersion = college.RowVersion;
            dto.UpdatedAt = college.UpdatedAt;
            dto.UpdatedByAppUserId = college.UpdatedByAppUserId;
            return dto;
        }

        public static ApplicationCore.Entities.College From(CollegeDto dto)
        {
            var college = new ApplicationCore.Entities.College();
            college.Id = dto.Id;
            college.CollegeName = dto.CollegeName;
            college.CreatedAt = dto.CreatedAt;
            college.CreatedByAppUserId = dto.CreatedByAppUserId;
            college.RowVersion = dto.RowVersion;
            college.UpdatedAt = dto.UpdatedAt;
            college.UpdatedByAppUserId = dto.UpdatedByAppUserId;
            return college;
        }
    }
    
}
