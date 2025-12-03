using ECommerce.Domain.Entities.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts
{
    public  interface IGenericRepository<TEntity, TKey> where TEntity :  BaseEntity<TKey>
    {

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity,TKey> specification);
        Task<TEntity?> GetByIdAsync(TKey Id);

        Task Add(TEntity entity);

        void Delete(TEntity entity);

        void Update (TEntity entity);


    }
}
