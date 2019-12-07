using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ICollegeRepository : IAsyncRepository<College>
    {
        Task<List<ApplicationCore.Entities.College>> GetCollegesFromNonAdmin(Guid appUserId);
    }
}
