using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.ProductModule;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Specifications
{
    public   abstract class BaseSpecifications<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {

        public Expression<Func<TEntity, bool>> Criteria { get; }
        public BaseSpecifications(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }

        public ICollection<Expression<Func<TEntity, object>>> includeExpression { get; }  = [];


        protected void AddInclude(Expression<Func<TEntity, object>> expression) { 
        
            includeExpression.Add(expression);
        }
    }
}
