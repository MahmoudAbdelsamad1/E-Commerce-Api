using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.ProductModule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Percistance.Repositories
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> EntryPoint,
            ISpecification<TEntity,TKey> specification) where TEntity : BaseEntity<TKey> {

            var query = EntryPoint;
            if(specification is not null)
            {
                if (specification.Criteria is not null) { 
                
                
                    query = query.Where(specification.Criteria);


                }


                if (specification.includeExpression is not null && specification.includeExpression.Any())
                {


                    foreach (var include in specification.includeExpression)
                    {

                        query =  query.Include(include);
                    }
                }

                //   query = specification.includeExpression.Aggregate(query, (currentQuery, incldueExption) => currentQuery.Include(incldueExption));

            }
            return query;
        }
    }
}
