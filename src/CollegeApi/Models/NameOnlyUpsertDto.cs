using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Api.Models
{
    public class NameOnlyUpsertDto : BaseDto
    {
        public byte[] RowVersion { get; set; }
        public string Name { get; set; }
    }
}
