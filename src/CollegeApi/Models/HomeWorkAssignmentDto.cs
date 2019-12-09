using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College.Api.Models
{
    public class HomeWorkAssignmentDto
    {
        public Guid YearClassId { get; set; }
        public DateTimeOffset DueDate { get; set; }

        public List<HomeWorkAssignmentItemDto> HomeWorkAssignmentItems = new List<HomeWorkAssignmentItemDto>();
        public List<SubmittedHomeWorkDto> SubmittedHomeWorks = new List<SubmittedHomeWorkDto>();

        public static HomeWorkAssignmentDto From(HomeWorkAssignment domainObject)
        {
            var dto = new HomeWorkAssignmentDto();
            dto.DueDate = domainObject.DueDate;
            dto.YearClassId = domainObject.YearClassId;
            dto.HomeWorkAssignmentItems = domainObject.HomeWorkAssignmentItems.Select(s => HomeWorkAssignmentItemDto.From(s)).ToList();
            dto.SubmittedHomeWorks = domainObject.SubmittedHomeWorks.Select(s => SubmittedHomeWorkDto.From(s)).ToList();
            return dto;
        }
    }

    public class HomeWorkAssignmentItemDto
    {
        public string Sentence { get; set; }
        public string Word { get; set; }
        public byte[] SpokenWordAsMp3 { get; set; }

        public static HomeWorkAssignmentItemDto From(HomeWorkAssignmentItem domainObject)
        {
            var dto = new HomeWorkAssignmentItemDto();
            dto.Sentence = domainObject.Sentence;
            dto.Word = domainObject.Word;
            dto.SpokenWordAsMp3 = domainObject.SpokenWordAsMp3;
            return dto;
        }
    }

    public class SubmittedHomeWorkDto
    {
        public string StudentName { get; set; }
        public int Score { get; set; }

        public static SubmittedHomeWorkDto From(SubmittedHomeWork domainObject)
        {
            var dto = new SubmittedHomeWorkDto();
            dto.StudentName = domainObject.StudentName;
            dto.Score = domainObject.Score;
            return dto;
        }

    }
}
