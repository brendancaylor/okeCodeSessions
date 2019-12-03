using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class TestController : ControllerBase
    {

        [HttpGet]
        public ActionResult<IdDto> GetSomethigs([FromQuery] string test)
        {
            return new IdDto { Id = 1 };
        }

    }
}