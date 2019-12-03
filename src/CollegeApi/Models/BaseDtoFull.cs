using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Api.Models
{
    public class BaseDtoFull : BaseDtoVersion
    {
        public Guid CreatedByAppUserId { get; set; }

        public Guid? UpdatedByAppUserId { get; set; }

    }
}
