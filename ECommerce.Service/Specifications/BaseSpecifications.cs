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
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
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

        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        protected void AddOrderByAscending(Expression<Func<TEntity, object>> expression)
        {

            OrderBy = expression;
        }

        public Expression<Func<TEntity, object>> OrderByDes { get; private set; }

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> expression)
        {

            OrderByDes = expression;
        }

        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool IsPaginated { get; private set; }


        public void AddPagination( int pageIndex , int pageSize) { 
        
            IsPaginated = true;

            Skip = (pageIndex - 1 ) * pageSize;
            Take = pageSize;
        }
    }
}
