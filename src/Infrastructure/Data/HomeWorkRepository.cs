using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class HomeWorkRepository : EfRepository<HomeWorkAssignment>, IHomeWorkRepository
    {
        public HomeWorkRepository(CollegeContext dbContext) : base(dbContext)
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

        public async Task<HomeWorkAssignment> GetHomeWorkAssignmentWithChildren(Guid homeWorkAssignmentId)
        {
            return await _dbContext
                .HomeWorkAssignments
                    .Include(o => o.HomeWorkAssignmentItems)
                    .Include(o => o.SubmittedHomeWorks)
                .SingleAsync(o => o.Id == homeWorkAssignmentId);
        }
    }
}