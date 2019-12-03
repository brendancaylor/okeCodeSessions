using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class CollegeRepository : EfRepository<College>, ICollegeRepository
    {
        public CollegeRepository(CollegeContext dbContext) : base(dbContext)
        {
        }

        //public Task<Order> GetByIdWithItemsAsync(int id)
        //{
        //    return _dbContext.Orders
        //        .Include(o => o.OrderItems)
        //        .Include($"{nameof(Order.OrderItems)}.{nameof(OrderItem.ItemOrdered)}")
        //        .FirstOrDefaultAsync(x => x.Id == id);
        //}
    }
}