using Microsoft.EntityFrameworkCore;
using NLayerProject.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Data.Repositories
{
          
    //Core katmanındaki IRepositryi implemen edeceğiz.
    class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        public readonly DbContext _context;
        public readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            //_context ile veri tabanına erişirim
            //_dbSet ile de tablolara erişmiş olurum, TEntitiy ne gelirse o Entity üzerinde işlem yapmış olacağım.
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.SingleOrDefaultAsync(predicate);
        }

        public TEntity Update(TEntity entity)
        {
            //tüm kolonlara update gönderir sen 1 kolon güncellesende bu method ile hepsi güncellenir. performans kaybı yasanır ama 
            //kod tekrarını engeller
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
