using System;
using System.Collections.Generic;
using System.Linq;
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
    [Authorize(Roles = "AdminisiterColleges")]
    public class CollegeController : BaseController
    {
        private readonly ICollegeRepository _collegeRepository;
        public CollegeController(ICollegeRepository collegeRepository)
        {
            _collegeRepository = collegeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<CollegeDto>>> GetAllCollegesAsync()
        {
            var data = await _collegeRepository.ListAllAsync();
            return data.Select(o => CollegeDto.From(o)).OrderBy(o => o.CollegeName).ToList();
        }

        [HttpGet("{collegeId}")]
        public async Task<ActionResult<CollegeDto>> GetCollegeAsync([FromRoute] Guid collegeId)
        {
            var college = await _collegeRepository.GetByIdAsync(collegeId);
            return CollegeDto.From(college);
        }

        [HttpPost]
        public async Task<ActionResult<SimpleUpsertDto>> AddCollegeAsync([FromBody] NameOnlyUpsertDto dto)
        {
            var college = new ApplicationCore.Entities.College
            {
                CollegeName = dto.Name
            };
            college = await _collegeRepository.AddAsync(college, this.AppUserId.Value);
            return SimpleUpsertDto.From(college);
        }

        [HttpPut]
        public async Task<ActionResult<SimpleUpsertDto>> UpdateCollegeAsync([FromBody] NameOnlyUpsertDto dto)
        {
            var college = await _collegeRepository.GetByIdAsync(dto.Id);
            _collegeRepository.SetRowVersion(college, dto.RowVersion);
            college.CollegeName = dto.Name;
            college = await _collegeRepository.UpdateAsync(college, this.AppUserId.Value);
            return SimpleUpsertDto.From(college);
        }

        //[HttpPut("update-users")]
        //public async Task<ActionResult<SimpleUpsertDto>> UpdateCollegeUsersAsync([FromBody] CollegeDto dto)
        //{
        //    var college = await _collegeRepository.GetByIdAsync(dto.Id);
        //    college.RowVersion = dto.RowVersion;
        //    college.CollegeName = dto.CollegeName;
        //    college = await _collegeRepository.UpdateAsync(college, this.AppUserId.Value);
        //    return SimpleUpsertDto.From(college);
        //}

    }
}
