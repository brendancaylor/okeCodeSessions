using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Api.Models
{
    public class UpdateUserDto : BaseDto
    {
        public string IdentityId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid RoleId { get; set; }

        public List<Guid> CollegeIds { get; set; }

        public static void SetAppUserFromDto(UpdateUserDto dto, AppUser appUser)
        {
            var collegeUsers = dto.CollegeIds.Select(s => new CollegeAppUser { AppUserId = dto.Id, CollegeId = s }).ToList();
            appUser.AddCollegeAppUsers(collegeUsers);
            appUser.Email = dto.Email;
            appUser.FirstName = dto.FirstName;
            appUser.Id = dto.Id;
            appUser.IdentityId = dto.IdentityId;
            appUser.LastName = dto.LastName;
            appUser.RoleId = dto.RoleId;
        }
        
    }
}
