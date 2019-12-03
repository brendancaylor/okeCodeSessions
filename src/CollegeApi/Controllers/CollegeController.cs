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
            return data.Select(o => CollegeDto.From(o)).ToList();
        }

        [HttpPost]
        public async Task<ActionResult<SimpleUpsertDto>> AddCollegeAsync([FromBody] AddCollegeDto dto)
        {
            var college = new ApplicationCore.Entities.College
            {
                CollegeName = dto.CollegeName
            };
            try
            {
                college = await _collegeRepository.AddAsync(college, this.AppUserId.Value);
            }
            catch (Exception ex)
            {

                throw;
            }
            
            return SimpleUpsertDto.From(college);
        }

    }
}
