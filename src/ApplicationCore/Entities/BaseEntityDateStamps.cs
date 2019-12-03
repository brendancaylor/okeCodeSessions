using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public abstract class BaseEntityDateStamps : BaseEntity
    {
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset? UpdatedAt { get; set; }

        public void SetDateAddProperties()
        {
            this.CreatedAt = DateTimeOffset.Now;
        }

        public void SetDateUpdateProperties()
        {
            this.UpdatedAt = DateTimeOffset.Now;
        }
    }
}
