using Microsoft.EntityFrameworkCore;
using Store.G04.Core.Entities;
using Store.G04.Core.Repositories.Contract;
using Store.G04.Core.Specifications;
using Store.G04.Ropository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Ropository.Repositories
{
    public class GenericRepository<TEntity, Tkey> : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntiy<Tkey>
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if (typeof(TEntity) == typeof(Product)) 
            {
              return (IEnumerable<TEntity>) await _context.Products.OrderBy(P =>P.Id).Include(P=>P.Brand).Include(P=>P.Type).ToListAsync();
            }
            return await _context.Set<TEntity>().ToListAsync();
        }
        public async Task<TEntity> GetAsync(Tkey id)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                //return await _context.Products.Include(P => P.Type).Include(P => P.Brand).FirstOrDefaultAsync(P => P.Id == id as int?) as TEntity;
                return await _context.Products.Where(P => P.Id==id as int?).Include(P => P.Type).Include(P => P.Brand).FirstOrDefaultAsync() as TEntity;
            }
            return await _context.Set<TEntity>().FindAsync(id);
        }
        public async Task AddAsync(TEntity entity)
        {
           await _context.AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }      
        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, Tkey> spec)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }

        public async Task<TEntity> GetWithSpecAsync(ISpecifications<TEntity, Tkey> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }

        private IQueryable<TEntity> ApplySpecifications(ISpecifications<TEntity, Tkey> spec) 
        {
           return  SpecificationEvaluator<TEntity, Tkey>.GetQuery(_context.Set<TEntity>(), spec);
        }

        public async Task<int> GetCountAsync(ISpecifications<TEntity, Tkey> spec)
        {
            return await ApplySpecifications(spec).CountAsync();
        }
    }
}
