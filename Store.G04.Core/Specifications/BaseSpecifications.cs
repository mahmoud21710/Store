using Store.G04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Specifications
{
    public class BaseSpecifications<TEntity ,TKey> : ISpecifications<TEntity, TKey>  where TEntity : BaseEntiy<TKey>
    {
        public Expression<Func<TEntity, bool>> Criteria { get; set; } = null;
        public List<Expression<Func<TEntity, object>>> IncludeS { get ; set ; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>> OrderBy { get; set; } = null;
        public Expression<Func<TEntity, object>> OrderByDescindig { get; set; } = null;
        public int Skip { get ; set ; }
        public int Take { get; set ; }
        public bool IsPaginationEnabled { get ; set; }

        public BaseSpecifications(Expression<Func<TEntity, bool>> expression)
        {
            Criteria = expression;
            //IncludeS = new List<Expression<Func<TEntity, object>>>();
        }
        public BaseSpecifications()
        {
            //Criteria = null;
            //IncludeS = new List<Expression<Func<TEntity, object>>>();
        }

        public void AddOrderBy(Expression<Func<TEntity, object>> expression) 
        {
            OrderBy = expression;
        }
        public void AddOrderByDesc(Expression<Func<TEntity, object>> expression) 
        {
            OrderByDescindig = expression;
        }
        public void ApplyPagination(int skip , int take) 
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }
    }
}
