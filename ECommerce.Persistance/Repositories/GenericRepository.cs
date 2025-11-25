using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Percistance.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Percistance.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _dbContext;

  

        public GenericRepository( StoreDbContext dbContext)
        {
             _dbContext = dbContext;
        }
        public async Task Add(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {

            _dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
         var result =  await   _dbContext.Set<TEntity>().ToListAsync();

            return result;
        }


        public async Task<TEntity?> GetByIdAsync(TKey Id)
        {
            var result = await _dbContext.Set<TEntity>().FindAsync(Id);
            return result;
        }

        public void Update(TEntity entity)
        {
           _dbContext.Set<TEntity>().Update(entity);
        }
    }
}
