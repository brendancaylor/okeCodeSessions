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
    public class AppUserRepository : EfRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(CollegeContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<AppUser>> GetAppUsersWithChildrenAsync(Guid? appUserId)
        {
            var query = _dbContext.AppUsers
                .Include(o => o.CollegeAppUsers);

            if(appUserId.HasValue)
            {
                var collegeAppUsers = await _dbContext.CollegeAppUsers
                    .Where(o => o.AppUserId == appUserId)
                    .ToListAsync();
                var collegeIds = collegeAppUsers.Select(s => s.CollegeId).ToList();
                var filteredData = await query.Where(o => o.CollegeAppUsers.Any(cao => collegeIds.Contains(cao.CollegeId))).ToListAsync();
                return filteredData;
            }
            var data = await query.ToListAsync();
            return data;
        }

        public Task<AppUser> GetAppUserWithChildrenAsync(Guid appUserId)
        {
            return _dbContext.AppUsers
                .Include(o => o.CollegeAppUsers)
                .SingleAsync(o => o.Id == appUserId);
        }

        public Task UpdateWithChildrenAsync(AppUser appUser, List<Guid> newCollegeIds)
        {
            appUser.SetDateUpdateProperties();
            foreach (var collegeAppUser in appUser.CollegeAppUsers)
            {
                _dbContext.Entry(collegeAppUser).State = EntityState.Deleted;
            }
            appUser.AddCollegeAppUsers(newCollegeIds);
            foreach (var collegeAppUser in appUser.CollegeAppUsers)
            {
                _dbContext.Entry(collegeAppUser).State = EntityState.Added;
            }
            return _dbContext.SaveChangesAsync();
        }
    }
}