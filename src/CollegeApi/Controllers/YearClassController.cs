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
    
    public class YearClassController : BaseController
    {
        private readonly IAsyncRepository<YearClass> _yearClassRepository;
        private readonly ICollegeRepository _collegeRepository;
        public YearClassController(
            IAsyncRepository<YearClass> yearClassRepository,
            ICollegeRepository collegeRepository)
        {
            _yearClassRepository = yearClassRepository;
            _collegeRepository = collegeRepository;
        }


        [HttpGet("college-lookups")]
        [Authorize]
        public async Task<List<LookupDto>> GetCollegeLookupsAsync()
        {
            var claims = User.Claims.Where(s => s.Type == "role").Select(s => s.Value).ToList();
            var canAdminisiterAllUsers = claims.Any(o => o.ToLower() == Constaints.ClaimAdminisiterAllUsers.ToLower());
            if (canAdminisiterAllUsers)
            {
                var collegeData = await _collegeRepository.ListAllAsync();
                return collegeData
                    .Select(o => new LookupDto { Id = o.Id, DisplayName = o.CollegeName})
                    .OrderBy(o => o.DisplayName)
                    .ToList();                
            }
            else
            {
                var collegeData = await _collegeRepository
                    .GetCollegesFromNonAdmin(this.AppUserId.Value);
                return collegeData
                    .Select(o => new LookupDto { Id = o.Id, DisplayName = o.CollegeName })
                    .OrderBy(o => o.DisplayName)
                    .ToList();                
            }
        }

        [HttpGet]
        [Authorize(Roles = "AdminisiterHomework")]
        public async Task<ActionResult<List<YearClassDto>>> GetYearClassesAsync([FromQuery] int academicYear, [FromQuery] Guid collegeId)
        {
            var data = await _yearClassRepository.ListAsync(o => o.AcademicYear == academicYear && o.CollegeId == collegeId);
            return data.Select(o => YearClassDto.From(o)).OrderBy(o => o.YearClassName).ToList();
        }

        [HttpPost]
        [Authorize(Roles = "AdminisiterHomework")]
        public async Task<SimpleUpsertDto> AddYearClassAsync([FromBody] YearClassAddDto dto)
        {
            var yearClass = YearClassAddDto.GetDomainObjectFrom(dto);
            yearClass = await _yearClassRepository.AddAsync(yearClass, this.AppUserId.Value);
            return SimpleUpsertDto.From(yearClass);
        }

        [HttpPut]
        [Authorize(Roles = "AdminisiterHomework")]
        public async Task<ActionResult<SimpleUpsertDto>> UpdateYearClassAsync([FromBody] YearClassUpdateDto dto)
        {
            var domainObject = await _yearClassRepository.GetByIdAsync(dto.Id);
            _yearClassRepository.SetRowVersion(domainObject, dto.RowVersion);
            YearClassUpdateDto.SetDomainObjectFrom(dto, domainObject);
            await _yearClassRepository.UpdateAsync(domainObject, this.AppUserId.Value);
            return SimpleUpsertDto.From(domainObject);
        }
    }
}
