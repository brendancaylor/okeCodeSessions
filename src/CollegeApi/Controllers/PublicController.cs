using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using College.Api.Models;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.TextToSpeech.V1;
using Grpc.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace College.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [AllowAnonymous]
    public class PublicController : BaseController
    {
        readonly IEmailSender _emailSender;

        public PublicController(
            IEmailSender emailSender
            )
        {
            _emailSender = emailSender;
        }

        [HttpPost("send-email")]
        public Task SendEmail([FromBody] SendEmailRequestDto dto)
        {
            var message = $"Message from : {dto.Name} {dto.Email} {dto.Telephone} {dto.Message}";
            return _emailSender.SendEmailAsync("brendan.caylor@gmail.com", "Spelling Contact Enquiry", message);
        }
    }
}
