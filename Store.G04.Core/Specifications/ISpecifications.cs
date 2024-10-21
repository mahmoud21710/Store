using Store.G04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Specifications
{
    public interface ISpecifications<TEntity , TKey> where TEntity : BaseEntiy<TKey>
    {
        public Expression<Func<TEntity,bool>>  Criteria  { get; set; }
        public List<Expression<Func<TEntity, object>>> IncludeS { get; set; }

        public Expression<Func<TEntity,object>> OrderBy { get; set; }
        public Expression<Func<TEntity,object>> OrderByDescindig { get; set; }

        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }

    }
    //Where(P => P.Id==id as int?).Include(P => P.Type)
    //.Include(P => P.Brand).FirstOrDefaultAsync() as TEntity;
}
