using ECommerce.Domain.Entities.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts
{
    public interface ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        ICollection<Expression<Func<TEntity, object>>> includeExpression { get; }

        Expression<Func<TEntity,bool>> Criteria { get; }

        Expression<Func<TEntity, object>> OrderBy { get; }

        Expression<Func<TEntity,object>> OrderByDes { get; }

        public int Skip { get; }
        public int Take { get; }
        public bool IsPaginated { get; }

    }
}
