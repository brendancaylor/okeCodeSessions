using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Api.Models
{
    public class BaseDtoVersion : BaseDtoDateStamps
    {
        public byte[] RowVersion { get; set; }
    }
}
