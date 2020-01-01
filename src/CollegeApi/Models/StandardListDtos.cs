using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Api.Models
{
    public class StandardListDto : BaseDto
    {
        public string StandardListName { get; set; }
        public string DefaultWordLanguage { get; set; }
        public string DefaultSentenceLanguage { get; set; }

        public List<StandardListItemDto> StandardListItems { get; set; }

        public static StandardListDto From(StandardList domainObject)
        {
            var dto = new StandardListDto();
            dto.Id = domainObject.Id;
            dto.DefaultSentenceLanguage = domainObject.DefaultSentenceLanguage;
            dto.DefaultWordLanguage = domainObject.DefaultWordLanguage;
            dto.StandardListName = domainObject.StandardListName;
            dto.StandardListItems = domainObject.StandardListItems.Select(StandardListItemDto.From).OrderBy(o => o.Word).ToList();
            //todo add items
            return dto;
        }

        public static StandardListDto FromNoSound(StandardList domainObject)
        {
            var dto = new StandardListDto();
            dto.Id = domainObject.Id;
            dto.DefaultSentenceLanguage = domainObject.DefaultSentenceLanguage;
            dto.DefaultWordLanguage = domainObject.DefaultWordLanguage;
            dto.StandardListName = domainObject.StandardListName;
            dto.StandardListItems = domainObject.StandardListItems.Select(StandardListItemDto.FromNoSound).OrderBy(o => o.Word).ToList();
            //todo add items
            return dto;
        }

        public static void From(StandardListDto dto, StandardList domainObject)
        {
            domainObject.Id = dto.Id;
            domainObject.DefaultSentenceLanguage = dto.DefaultSentenceLanguage;
            domainObject.DefaultWordLanguage = dto.DefaultWordLanguage;
            domainObject.StandardListName = dto.StandardListName;
        }
    }



    public class StandardListItemDto : BaseDto
    {
        public Guid StandardListId { get; set; }
        public string Sentence { get; set; }
        public string Word { get; set; }
        public string WordLanguage { get; set; }
        public string SentenceLanguage { get; set; }

        public static StandardListItemDto From(StandardListItem domainObject)
        {
            var dto = new StandardListItemDto();
            dto.StandardListId = domainObject.StandardListId;
            dto.Id = domainObject.Id;
            dto.Word = domainObject.Word;
            dto.Sentence = domainObject.Sentence;
            dto.WordLanguage = domainObject.WordLanguage;
            dto.SentenceLanguage = domainObject.SentenceLanguage;
            return dto;
        }

        public static StandardListItemDto FromNoSound(StandardListItem domainObject)
        {
            var dto = new StandardListItemDto();
            dto.StandardListId = domainObject.StandardListId;
            dto.Id = domainObject.Id;
            dto.Word = domainObject.Word;
            dto.Sentence = domainObject.Sentence;
            dto.WordLanguage = domainObject.WordLanguage;
            dto.SentenceLanguage = domainObject.SentenceLanguage;
            return dto;
        }

        public static void From(StandardListItemDto dto, StandardListItem domainObject)
        {
            domainObject.Id = dto.Id;
            domainObject.StandardListId = dto.StandardListId;
            domainObject.SentenceLanguage = dto.SentenceLanguage;
            domainObject.WordLanguage = dto.WordLanguage;
            domainObject.Word = dto.Word;
            domainObject.Sentence = dto.Sentence;
            domainObject.WordLanguage = dto.WordLanguage;
            domainObject.SentenceLanguage = dto.SentenceLanguage;
        }


    }
}
