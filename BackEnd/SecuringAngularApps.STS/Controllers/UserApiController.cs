using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecuringAngularApps.STS.Models;

namespace SecuringAngularApps.STS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : Controller
    {
        readonly UserManager<ApplicationUser> _userManager;

        public UserApiController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] IdentityServiceApiAddUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return Json(string.Empty);
            }

            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                EmailConfirmed = true,
                LockoutEnabled = false,
            };

            var result = await _userManager.CreateAsync(user);
            //var result2 = await _userManager.ResetPasswordAsync(user, password);
            if (!result.Succeeded)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return Json(string.Empty);
            }

            Response.StatusCode = StatusCodes.Status201Created;
            return Json(user.Id);
        }

        [HttpGet]
        [Route("user-token-for-password-reset")]
        public async Task<IActionResult> UserForPasswordReset(string emailAddress)
        {
            var user = await _userManager.FindByEmailAsync(emailAddress.ToLower());
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return Json(new IdentityServiceApiUserTokenForPasswordResetDto { Token = token, UserId = user.Id });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("set-new-password")]
        public async Task<bool> SetNewPassword([FromBody] IdentityServiceApiSetNewPasswordRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == dto.UserId);
            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.Password);
            return result.Succeeded;
        }

    }
}