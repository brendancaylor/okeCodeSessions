using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Projections;
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
        private readonly IAppUserRepository _userRepository;
        private readonly ICollegeRepository _collegeRepository;
        private readonly IAsyncRepository<Role> _roleRepository;
        private HttpClient _httpClient;
        public UserController(
            IAppUserRepository userRepository,
            ICollegeRepository collegeRepository,
            IAsyncRepository<Role> roleRepository,
            IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("identityClient");
            _userRepository = userRepository;
            _collegeRepository = collegeRepository;
            _roleRepository = roleRepository;
        }

        [HttpGet("usage-report")]
        [AllowAnonymous]
        public async Task<ActionResult<List<CollegeUsage>>> GetUsageReportAsync(Guid? collegeId)
        {
            var data = await _collegeRepository.GetCollegesUsage(collegeId);
            return data.OrderBy(o => o.WordSum).ThenByDescending(o => o.AcademicYear).ToList();
        }

        [HttpGet("get-current-user-claims")]
        [Authorize]
        public CurrentUsersClaimsDto GetCurrentUserClaims()
        {
            var dto = new CurrentUsersClaimsDto();
            dto.Claims = User.Claims.Where(s => s.Type == "role").Select(s => s.Value).ToList();
            return dto;
        }

        [HttpGet("user-lookups")]
        [Authorize]
        public async Task<UserLookupsDto> GetUserLookupsAsync()
        {
            var dto = new UserLookupsDto();
            var claims = User.Claims.Where(s => s.Type == "role").Select(s => s.Value).ToList();
            var canAdminisiterAllUsers = claims.Any(o => o.ToLower() == Constaints.ClaimAdminisiterAllUsers.ToLower());
            if (canAdminisiterAllUsers)
            {
                var collegeData = await _collegeRepository.ListAllAsync();
                dto.Colleges = collegeData
                    .Select(o => new LookupDto { Id = o.Id, DisplayName = o.CollegeName})
                    .OrderBy(o => o.DisplayName)
                    .ToList();
                
                var roleData = await this._roleRepository.ListAllAsync();
                dto.Roles = roleData
                    .Select(o => new LookupDto { Id = o.Id, DisplayName = o.DisplayName })
                    .OrderBy(o => o.DisplayName)
                    .ToList();
            }
            else
            {
                var collegeData = await _collegeRepository
                    .GetCollegesFromNonAdmin(this.AppUserId.Value);
                dto.Colleges = collegeData
                    .Select(o => new LookupDto { Id = o.Id, DisplayName = o.CollegeName })
                    .OrderBy(o => o.DisplayName)
                    .ToList();
                
                var roleData = await this._roleRepository.ListAllAsync();
                dto.Roles = roleData
                    .Where(o => o.RoleName != Constaints.RoleFullAdmin)
                    .Select(o => new LookupDto { Id = o.Id, DisplayName = o.DisplayName })
                    .OrderBy(o => o.DisplayName)
                    .ToList();
            }

            return dto;
        }

        [HttpGet]
        [Authorize(Roles = "AdminisiterCollegeUsers")]
        public async Task<ActionResult<List<UserDto>>> GetAllUsersAsync()
        {
            Guid? appUserId = null;
            var claims = User.Claims.Where(s => s.Type == "role").Select(s => s.Value).ToList();
            var canAdminisiterAllUsers = claims.Any(o => o.ToLower() == Constaints.ClaimAdminisiterAllUsers.ToLower());
            if(!canAdminisiterAllUsers)
            {
                appUserId = this.AppUserId;
            }
            var data = await _userRepository.GetAppUsersWithChildrenAsync(appUserId);
            return data.Select(o => UserDto.From(o)).OrderBy(o => o.LastName).ToList();
        }

        [HttpPost]
        [Authorize(Roles = "AdminisiterCollegeUsers")]
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

        [HttpPut]
        [Authorize(Roles = "AdminisiterCollegeUsers")]
        public async Task<ActionResult<SimpleUpsertDto>> UpdateUserAsync([FromBody] UpdateUserDto dto)
        {
            var appUser = await _userRepository.GetAppUserWithChildrenAsync(dto.Id);
            //UpdateUserDto.SetAppUserFromDto(dto, user);
            //appUser.AddCollegeAppUsers(dto.CollegeIds);
            appUser.Email = dto.Email;
            appUser.FirstName = dto.FirstName;
            appUser.LastName = dto.LastName;
            appUser.RoleId = dto.RoleId;
            await _userRepository.UpdateWithChildrenAsync(appUser, dto.CollegeIds);
            return SimpleUpsertDto.From(appUser);
        }
    }
}
