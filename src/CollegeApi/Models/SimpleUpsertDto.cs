using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Api.Models
{
    public class SimpleUpsertDto : BaseDto
    {
        public byte[] RowVersion { get; set; }

        public static SimpleUpsertDto From(ApplicationCore.Entities.BaseEntityFull baseClass)
        {
            var dto = new SimpleUpsertDto();
            dto.Id = baseClass.Id;
            dto.RowVersion = baseClass.RowVersion;
            return dto;
        }

        public static SimpleUpsertDto From(ApplicationCore.Entities.BaseEntityDateStamps baseClass)
        {
            var dto = new SimpleUpsertDto();
            dto.Id = baseClass.Id;
            return dto;
        }

    }

    
}
