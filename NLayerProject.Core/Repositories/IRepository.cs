using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Core.Repositories
{
    interface IRepository<TEntity> where TEntity:class
    {
        //id ye göre çek
        Task<TEntity> GetByIdAsync(int Id);
        //tüm veriler
        Task<IEnumerable<TEntity>> GetAllAsync();
        //find(x=>x.id=2) diye çağırcaz bu metodu
        Task <IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
        //category.SingleOrDefault(x=>x.name="kalem") delfault olarak ilk olanı gelecek.
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        //bir kayıt kaydet
        Task AddAsync(TEntity entity);
        //birden fazla kayıt kaydet
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        //sil
        void Remove(TEntity entity);
        //toplu sil
        void RemoveRange(IEnumerable<TEntity> entities);
        //update et modeli don
        TEntity Update(TEntity entity);

    }
}
