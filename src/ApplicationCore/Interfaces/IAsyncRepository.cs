using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> AddAsync(T entity, Guid appUserId);
        Task UpdateAsync(T entity, Guid appUserId);
        Task DeleteAsync(T entity);
        Task<int> CountAsync();
    }
}
