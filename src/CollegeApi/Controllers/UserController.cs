using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using College.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace College.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    
    public class UserController : BaseController
    {

        private readonly IAsyncRepository<AppUser> _userRepository;
        private HttpClient _httpClient;
        public UserController(IAsyncRepository<AppUser> userRepository, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("identityClient");
            _userRepository = userRepository;
        }

        [HttpGet("get-current-user-claims")]
        [Authorize]
        public CurrentUsersClaimsDto GetCurrentUserClaims()
        {
            var dto = new CurrentUsersClaimsDto();
            dto.Claims = User.Claims.Where(s => s.Type == "role").Select(s => s.Value).ToList();
            return dto;
        }

        [HttpPost]
        [Authorize(Roles = "AdminisiterAllUsers")]
        public async Task<SimpleUpsertDto> AddUserAsync([FromBody] AddUserDto dto)
        {
            var appUser = AddUserDto.GetAddUserFrom(dto);
            appUser = await _userRepository.AddAsync(appUser, this.AppUserId.Value);

            var identityDto = new IdentityServiceApiAddUserDto { Email = dto.Email };

            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(identityDto));
            content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("api/UserApi", content);
            response.EnsureSuccessStatusCode();
            string identityId = await response.Content.ReadAsStringAsync();
            appUser.IdentityId = identityId;
            appUser = await _userRepository.UpdateAsync(appUser, this.AppUserId.Value);
            return SimpleUpsertDto.From(appUser);
        }
    }
}
