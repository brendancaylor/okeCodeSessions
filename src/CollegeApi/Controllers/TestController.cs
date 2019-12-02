using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace College.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class TestController : ControllerBase
    {

        [HttpGet]
        public ActionResult<string> GetIdTest3([FromQuery] string test)
        {
            return test;
        }

    }
}