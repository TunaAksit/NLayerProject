using NLayerProject.Core.Repositories;
using NLayerProject.Core.UnitOfWorks;
using NLayerProject.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Data.UnitOfWorks
{
    class UnitOfWork : IUnitOfWork

    {
        private readonly AppDbContext _context;

        private ProductRepository _productRepository;
        private CategoryRepository _categoryRepository;
        //_productRepository verieceğim eğer ?? null ise git _productRepository = new ProductRepository oluştur ata
        public IProductRepository Products => _productRepository = _productRepository ?? new ProductRepository(_context);
        //_categoryRepository verieceğim eğer ?? null ise git _categoryRepository = new CategoryRepository oluştur ata
        public ICategoryRepository Categories => _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);

        public UnitOfWork(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
