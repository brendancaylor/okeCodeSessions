using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Api.Models
{
    public class UserDto : BaseDto
    {
        public string IdentityId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid RoleId { get; set; }
        public List<Guid> CollegeIds { get; set; }

        public static UserDto From(AppUser appUser)
        {
            var dto = new UserDto();
            dto.CollegeIds = appUser.CollegeAppUsers.Select(s => s.CollegeId).ToList();
            dto.Email = appUser.Email;
            dto.FirstName = appUser.FirstName;
            dto.IdentityId = appUser.IdentityId;
            dto.LastName = appUser.LastName;
            dto.RoleId = appUser.RoleId;
            return dto;
        }
    }
}
