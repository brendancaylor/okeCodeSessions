using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Api.Models
{
    public class IdDto
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
