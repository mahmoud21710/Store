using Store.G04.Core.Entities;
using Store.G04.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core
{
    public interface IUnitOfWork
    {
        //Create Repository<> And Return 
        IGenericRepository<TEntity,Tkey> CreateRepository<TEntity,Tkey>() where TEntity : BaseEntiy<Tkey>;
        Task<int> CompleteAsync();
    }
}
