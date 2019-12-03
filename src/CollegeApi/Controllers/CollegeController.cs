using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class CollegeController : ControllerBase
    {
        private readonly ICollegeRepository _collegeRepository;
        public CollegeController(ICollegeRepository collegeRepository)
        {
            _collegeRepository = collegeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<CollegeDto>>> GetAllCollegesAsync([FromQuery] string test)
        {
            var data = await _collegeRepository.ListAllAsync();
            return data.Select(o => CollegeDto.From(o)).ToList();
        }
    }
}
