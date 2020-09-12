using NLayerProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Core.Repositories
{
    public interface ICategoryRepository:IRepository<Category>
    {
        //IRepositoryi miras aldık. IRepository metotlarda default içinde gelecek
        //Ideye bağlı grubu ve baglı ürüleri dön
        Task<Category> GetWithProductByIdAsync(int categoryId);


    }
}
