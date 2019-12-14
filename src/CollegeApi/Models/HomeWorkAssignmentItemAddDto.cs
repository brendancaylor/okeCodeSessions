using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Api.Models
{
    public class HomeWorkAssignmentItemAddDto : HomeWorkAssignmentItemBaseDto
    {
        public static HomeWorkAssignmentItem GetDomainObjectFrom(HomeWorkAssignmentItemAddDto dto)
        {
            var domainObject = new HomeWorkAssignmentItem();
            domainObject.HomeWorkAssignmentId = dto.HomeWorkAssignmentId;
            domainObject.Sentence = dto.Sentence;
            domainObject.Word = dto.Word;
            domainObject.SentenceLanguage = dto.SentenceLanguage;
            domainObject.WordLanguage = dto.WordLanguage;
            return domainObject;
        }
    }
}
