﻿using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Api.Models
{
    public class AddUserDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid RoleId { get; set; }
        public List<Guid> CollegeIds { get; set; }

        public static AppUser GetAddUserFrom(AddUserDto dto)
        {
            var appUser = new AppUser();
            var collegeUsers = dto.CollegeIds.Select(s => new CollegeAppUser { AppUserId = appUser.Id, CollegeId = s }).ToList();
            appUser.AddCollegeAppUsers(collegeUsers);
            appUser.Email = dto.Email;
            appUser.FirstName = dto.FirstName;
            appUser.IdentityId = Guid.Empty.ToString();
            appUser.LastName = dto.LastName;
            appUser.RoleId = dto.RoleId;
            return appUser;
        }
    }
}
