using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        void SetRowVersion(T objectToSet, byte[] rowVersion);
        Task<T> GetByIdAsync(Guid id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> criteria);
        Task<T> AddAsync(T entity, Guid appUserId);
        Task<T> UpdateAsync(T entity, Guid appUserId);
        Task DeleteAsync(T entity);
        Task<int> CountAsync();
    }
}
