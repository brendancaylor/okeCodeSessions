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
    public class StandardListRepository : EfRepository<StandardList>, IStandardListRepository
    {
        public StandardListRepository(CollegeContext dbContext) : base(dbContext)
        {
        }

        public async Task<StandardList> GetStandardListWithChildrenAsync(Guid standardListId)
        {
            return await _dbContext
                .StandardLists
                    .Include(o => o.StandardListItems)
                .SingleAsync(o => o.Id == standardListId);
        }

        public async Task<bool> ReplaceStandardListItemsAsync(Guid standardListId, List<StandardListItem> standardListItems)
        {
            try
            {
                var standardList = await _dbContext
                    .StandardLists
                    .Include(o => o.StandardListItems)
                .SingleAsync(o => o.Id == standardListId);

                var itemsToRemove = standardList.StandardListItems.Select(s => s).ToList();

                foreach (var item in itemsToRemove)
                {
                    _dbContext.Set<StandardListItem>().Remove(item);
                }

                foreach (var standardListItem in standardListItems)
                {
                    standardList.AddStandardListItem(standardListItem);
                    await _dbContext.Set<StandardListItem>().AddAsync(standardListItem);
                }
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}