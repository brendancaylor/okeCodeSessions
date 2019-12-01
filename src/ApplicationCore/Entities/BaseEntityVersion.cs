using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public abstract class BaseEntityVersion : BaseEntityDateStamps
    {
        public byte[] RowVersion { get; set; }
    }
}
