using ApplicationCore.Entities;
using ApplicationCore.Projections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ICollegeRepository : IAsyncRepository<College>
    {
        Task<List<ApplicationCore.Entities.College>> GetCollegesFromNonAdmin(Guid appUserId);
        Task<List<CollegeUsage>> GetCollegesUsage(Guid? collegeId);
    }
}
