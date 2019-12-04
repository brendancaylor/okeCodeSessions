using ApplicationCore;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using IdentityServer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    public class PasswordRecoveryController : Controller
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly IEmailSender _emailSender;
        readonly IIdentityApiConfirguration _options;

        public PasswordRecoveryController(
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            IOptions<IdentityApiConfirguration> options)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _options = options.Value;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Submitted = false;
            ViewBag.ReturnUrl = _options.SpaSpellingClientBaseUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RequestPasswordResetDto dto)
        {
            ViewBag.ReturnUrl = _options.SpaSpellingClientBaseUrl;
            if (this.ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(dto.Emailaddress.ToLower());
                if(user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var host = this.Request.Host.Value.Replace("[", string.Empty).Replace("]", string.Empty);
                    var url = $"{this.Request.Scheme}://{host}/PasswordRecovery/ResetPassword?token={WebUtility.UrlEncode(token)}";

                    var message = $"A password reset has be requested.<br /> <a href=\"{url}\">click here to reset your password.</a>";
                    await _emailSender.SendEmailAsync(user.Email, "College Spelling App Password Reset", message);
                }
            }
            ViewBag.Submitted = true;
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token)
        {
            RequestPasswordChangeDto dto = new RequestPasswordChangeDto
            {
                Token = token
            };
            return View(dto);
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(RequestPasswordChangeDto dto)
        {
            if (!this.ModelState.IsValid)
            {
                return View(dto);
            }
            var user = await _userManager.FindByEmailAsync(dto.Emailaddress.ToLower());
            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);

                if (!result.Succeeded)
                {
                    dto.IdentityResult = result;
                    return View(dto);
                }
            }
            else
            {
                ModelState.AddModelError("Emailaddress", "You have not entered your email address correctly.");
                return View(dto);
            }

            ViewBag.ReturnUrl = _options.SpaSpellingClientBaseUrl;
            return View("PasswordWasReset");
        }
    }
}
