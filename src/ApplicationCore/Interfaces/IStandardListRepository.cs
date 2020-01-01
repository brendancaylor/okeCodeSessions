using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IStandardListRepository : IAsyncRepository<StandardList>
    {
        Task<StandardList> GetStandardListWithChildrenAsync(Guid standardListId);
        Task<bool> ReplaceStandardListItemsAsync(Guid standardListId, List<StandardListItem> standardListItems);
    }
}
