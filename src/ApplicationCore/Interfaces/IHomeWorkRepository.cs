using ApplicationCore.Entities;
using ApplicationCore.Projections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IHomeWorkRepository : IAsyncRepository<HomeWorkAssignment>
    {
        Task<HomeWorkAssignment> GetHomeWorkAssignmentWithChildrenAsync(Guid homeWorkAssignmentId);
        Task AddHomeworkFromListAsync(AddHomeworkFromList addHomeworkFromList, Guid appUserId);
    }
}
