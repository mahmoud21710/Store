using Microsoft.EntityFrameworkCore;
using Store.G04.Core.Entities;
using Store.G04.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Ropository
{
    public static class SpecificationEvaluator<TEntity ,TKey> where TEntity : BaseEntiy<TKey>
    {
        //Create ANd return Query
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery 
            ,ISpecifications<TEntity,TKey> spec) 
        {
            var query = inputQuery;

            if (spec.Criteria is not null) 
            {
                 query = query.Where(spec.Criteria);
            }
            if (spec.OrderBy is not null) 
            {
                 query = query.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDescindig is not null) 
            {
                query = query.OrderByDescending(spec.OrderByDescindig);
            }
            if (spec.IsPaginationEnabled) 
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }
            // Aggregate 

            query = spec.IncludeS.Aggregate(query, (currentquery, includeexpression) 
                => currentquery.Include(includeexpression));

            return query;
        }
    }
    //_context.Products..Where(P => P.Id==id as int?).Include(P => P.Type)
    //.Include(P => P.Brand).FirstOrDefaultAsync() as TEntity;
}
