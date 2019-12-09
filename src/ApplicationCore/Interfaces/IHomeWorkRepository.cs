using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IHomeWorkRepository : IAsyncRepository<HomeWorkAssignment>
    {
        Task<HomeWorkAssignment> GetHomeWorkAssignmentWithChildren(Guid homeWorkAssignmentId);
    }
}
