using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace College.Api.Models
{
    public class CollegeDto : BaseDto
    {
        public string CollegeName { get; set; }

        public static CollegeDto From(ApplicationCore.Entities.College college)
        {
            var dto = new CollegeDto();
            dto.Id = college.Id;
            dto.CollegeName = college.CollegeName;
            return dto;
        }
    }
    
}
