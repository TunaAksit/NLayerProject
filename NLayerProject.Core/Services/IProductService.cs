using NLayerProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Core.Services
{
    interface IProductService:IService<Product>
    {
        Task<Product> GetWithCategoryByIdAsync(int productId);
        // dbye gitmeden barcde kontrol edebiliriz
        //   bool ControlInnerBarcode(Product product);
    }
}
