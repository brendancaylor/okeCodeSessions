using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        public Guid? AppUserId
        { 
            get
            {
                var appUserId = User?.Claims?.FirstOrDefault(o => o.Type.ToLower() == "appuserid")?.Value;
                if(!string.IsNullOrWhiteSpace(appUserId))
                {
                    return Guid.Parse(appUserId);
                }
                return null;
            }   
        }
        
    }
}
