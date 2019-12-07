using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppUserRepository : IAsyncRepository<AppUser>
    {
        Task<List<AppUser>> GetAppUsersWithChildrenAsync(Guid? appUserId);
        Task<AppUser> GetAppUserWithChildrenAsync(Guid appUserId);
        Task UpdateWithChildrenAsync(AppUser appUser, List<Guid> newCollegeIds);
    }
}
