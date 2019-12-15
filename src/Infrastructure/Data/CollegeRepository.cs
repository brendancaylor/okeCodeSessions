using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Projections;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class CollegeRepository : EfRepository<College>, ICollegeRepository
    {
        public CollegeRepository(CollegeContext dbContext) : base(dbContext)
        {
        }

        public Task<List<College>> GetCollegesFromNonAdmin(Guid appUserId)
        {
            return _dbContext
                .CollegeAppUsers
                    .Include(o => o.College)
                .Where(o => o.AppUserId == appUserId)
                .Select(s => s.College)
                .ToListAsync();
        }

        public Task<List<CollegeUsage>> GetCollegesUsage()
        {
            return _dbContext
                .GoogleSpeechApiRequests
                    .Include(o => o.HomeWorkAssignmentItem.HomeWorkAssignment.YearClass.College)
                .GroupBy(o =>
                    new {
                        o.HomeWorkAssignmentItem.HomeWorkAssignment.YearClass.YearClassName,
                        o.HomeWorkAssignmentItem.HomeWorkAssignment.YearClass.AcademicYear,
                        o.HomeWorkAssignmentItem.HomeWorkAssignment.YearClass.TeacherName,
                        o.HomeWorkAssignmentItem.HomeWorkAssignment.YearClass.CollegeId,
                        o.HomeWorkAssignmentItem.HomeWorkAssignment.YearClass.College.CollegeName
                    }
                )
                .Select(s => 
                    new CollegeUsage {
                        WordSum = s.Sum(i => i.WordCount),
                        SentenceSum = s.Sum(i => i.SentenceCount),
                        YearClassName = s.Key.YearClassName,
                        AcademicYear = s.Key.AcademicYear,
                        TeacherName = s.Key.TeacherName,
                        CollegeId = s.Key.CollegeId,
                        CollegeName = s.Key.CollegeName
                    }
                )
                .ToListAsync();
        }
    }
}