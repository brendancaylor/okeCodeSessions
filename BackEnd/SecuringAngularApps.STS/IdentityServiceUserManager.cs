using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SecuringAngularApps.STS.Data;
using SecuringAngularApps.STS.Models;

namespace SecuringAngularApps.STS
{
    public class IdentityServiceUserManager
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly ApplicationDbContext _dbContext;
        readonly SignInManager<ApplicationUser> _signInManager;

        public IdentityServiceUserManager(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _signInManager = signInManager;
        }

        public async Task<IdentityServiceApiSetNewPasswordRequestDto> SetNewPassword(IdentityServiceApiSetNewPasswordRequestDto dto)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == dto.UserId);
            if (user == null)
            {
                dto.Error = "Request not authorised";
                return dto;
            }
            var result = await ResetPasswordAsync(user, dto.Token, dto.Password);
            if (result.Succeeded)
            {
                return dto;
            }

            dto.Error = result.Errors.Select(e => e.Description == "Invalid token."
                    ? "This password request is out-of-date or invalid; you need a new one."
                    : e.Description).First();

            if (string.IsNullOrWhiteSpace(dto.Error))
                dto.Error = "Unknown error";

            return dto;
        }



        async Task<bool> NewPasswordSameAsCurrent(ApplicationUser user, string password)
        {
            var isNewPasswordSameAsCurrent = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);
            if (isNewPasswordSameAsCurrent.Succeeded) return true;
            return false;
        }

        async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string password)
        {
            var oldPasswordHash = user.PasswordHash;

            var result = await _userManager.ResetPasswordAsync(user, token, password);

            if (!result.Succeeded) return result;

            if (oldPasswordHash != null)
            {
                await _dbContext.SaveChangesAsync();
            }

            return result;
        }

        public void EnsureUser(string emailAddress, string password)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Email == emailAddress);

            if (user == null)
            {
                var newUser = new ApplicationUser
                {
                    Email = emailAddress,
                    UserName = emailAddress,
                    EmailConfirmed = true
                };

                var createUserResult = _userManager.CreateAsync(newUser, password).Result;

                if (!createUserResult.Succeeded)
                {
                    throw new Exception($"Failed to create user with email address {emailAddress}");
                }
            }
        }


    }
}
