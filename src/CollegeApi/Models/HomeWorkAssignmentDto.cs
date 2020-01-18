using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Api.Models
{
    public class HomeWorkAssignmentDto: BaseDtoVersion
    {
        public Guid YearClassId { get; set; }
        public string YearClassDisplay { get; set; }
        public DateTimeOffset DueDate { get; set; }

        public ICollection<HomeWorkAssignmentItemDto> HomeWorkAssignmentItems { get; set; } = new List<HomeWorkAssignmentItemDto>();
        public ICollection<SubmittedHomeWorkDto> SubmittedHomeWorks { get; set; } = new List<SubmittedHomeWorkDto>();

        public int CountSubmissions { get; set; }

        public string firstWord { get; set; }
        public string lastWord { get; set; }

        public static HomeWorkAssignmentDto From(HomeWorkAssignment domainObject, bool fullyLoaded)
        {
            var dto = new HomeWorkAssignmentDto();
            dto.Id = domainObject.Id;
            dto.CreatedAt = domainObject.CreatedAt;
            dto.RowVersion = domainObject.RowVersion;
            dto.UpdatedAt = domainObject.UpdatedAt;
            dto.DueDate = domainObject.DueDate;
            dto.YearClassDisplay = $"{domainObject.YearClass?.TeacherName} {domainObject.YearClass?.YearClassName}";
            dto.YearClassId = domainObject.YearClassId;
            if (fullyLoaded)
            {
                dto.HomeWorkAssignmentItems = domainObject.HomeWorkAssignmentItems.Select(s => HomeWorkAssignmentItemDto.From(s)).OrderBy(o => o.Word).ToList();
                dto.SubmittedHomeWorks = domainObject.SubmittedHomeWorks.Select(s => SubmittedHomeWorkDto.From(s)).OrderBy(o => o.StudentName).ToList();
            }
            else
            {
                dto.firstWord = domainObject.HomeWorkAssignmentItems.OrderBy(o => o.Word).FirstOrDefault()?.Word;
                dto.lastWord = domainObject.HomeWorkAssignmentItems.OrderByDescending(o => o.Word).FirstOrDefault()?.Word;
                if(dto.firstWord == dto.lastWord)
                {
                    dto.lastWord = null;
                }
                dto.CountSubmissions = domainObject.SubmittedHomeWorks.Count();
            }
            return dto;
        }
    }

    public class HomeWorkAssignmentItemDto : BaseDtoVersion
    {
        public string Sentence { get; set; }
        public string Word { get; set; }
        public string SentenceLanguage { get; set; }
        public string WordLanguage { get; set; }
        public byte[] SpokenWordAsMp3 { get; set; }
        public byte[] SpokenSentenceAsMp3 { get; set; }
        
        public static HomeWorkAssignmentItemDto From(HomeWorkAssignmentItem domainObject)
        {
            var dto = new HomeWorkAssignmentItemDto();
            dto.Id = domainObject.Id;
            dto.CreatedAt = domainObject.CreatedAt;
            dto.RowVersion = domainObject.RowVersion;
            dto.UpdatedAt = domainObject.UpdatedAt;
            dto.Sentence = domainObject.Sentence;
            dto.Word = domainObject.Word;
            dto.SentenceLanguage = domainObject.SentenceLanguage;
            dto.WordLanguage = domainObject.WordLanguage;
            //dto.SpokenWordAsMp3 = domainObject.SpokenWordAsMp3;
            //dto.SpokenSentenceAsMp3 = domainObject.SpokenSentenceAsMp3;
            return dto;
        }
    }

    public class SubmittedHomeWorkDto : BaseDtoVersion
    {
        public string StudentName { get; set; }
        public int Score { get; set; }

        public static SubmittedHomeWorkDto From(SubmittedHomeWork domainObject)
        {
            var dto = new SubmittedHomeWorkDto();
            dto.Id = domainObject.Id;
            dto.StudentName = domainObject.StudentName;
            dto.Score = domainObject.Score;
            return dto;
        }

    }
}
