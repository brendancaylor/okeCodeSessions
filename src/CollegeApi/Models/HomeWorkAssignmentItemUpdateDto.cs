using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Api.Models
{
    public class HomeWorkAssignmentItemUpdateDto : HomeWorkAssignmentItemBaseDto
    {
        public Guid Id { get; set; }
        public byte[] RowVersion { get; set; }

        public static void SetDomainObjectFrom(HomeWorkAssignmentItemUpdateDto dto, HomeWorkAssignmentItem domainObject)
        {
            domainObject.HomeWorkAssignmentId = dto.HomeWorkAssignmentId;
            domainObject.Sentence = dto.Sentence;
            domainObject.Word = dto.Word;
        }
    }
}
