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
    public class EfRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly CollegeContext _dbContext;

        public EfRepository(CollegeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SetRowVersion(T objectToSet, byte[] rowVersion)
        {
            _dbContext.Entry(objectToSet).Property("RowVersion").OriginalValue = rowVersion;
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }


        public async Task<int> CountAsync()
        {
            return await _dbContext.Set<T>().CountAsync();
        }

        public async Task<T> AddAsync(T entity, Guid appUserId)
        {
            await _dbContext.Set<T>().AddAsync(entity);

            if(typeof(T).IsSubclassOf(typeof(BaseEntityFull)))
            {
                var baseEntity = entity as BaseEntityFull;
                baseEntity.SetUserAddProperties(appUserId);
            }

            if (typeof(T).IsSubclassOf(typeof(BaseEntityDateStamps)))
            {
                var baseEntity = entity as BaseEntityDateStamps;
                baseEntity.SetDateAddProperties();
            }

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
            

            return entity;
        }

        public async Task<T> UpdateAsync(T entity, Guid appUserId)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            if (typeof(T).IsSubclassOf(typeof(BaseEntityFull)))
            {
                var baseEntity = entity as BaseEntityFull;
                baseEntity.SetUserUpdateProperties(appUserId);
            }

            if (typeof(T).IsSubclassOf(typeof(BaseEntityDateStamps)))
            {
                var baseEntity = entity as BaseEntityDateStamps;
                baseEntity.SetDateUpdateProperties();
            }
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

    }
}
