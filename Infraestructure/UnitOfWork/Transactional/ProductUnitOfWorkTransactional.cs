using Domain.Interfaces.Repositories.Transactional;
using Domain.Interfaces.UnitOfWork.Transactional;
using Infraestructure.Data;
using Infraestructure.Repositories.Transactional;

namespace Infraestructure.UnitOfWork.Transactional
{
    public class ProductUnitOfWorkTransactional : IProductUnitOfWorkTransactional
    {
        private readonly AppDbContext _context;
        private IProductRepositoryTransactional? _productRepositoryTransactional;

        public ProductUnitOfWorkTransactional(AppDbContext context)
        {
            _context = context;
        }

        public IProductRepositoryTransactional ProductRepositoryTransactional
        {
            get
            {
                return _productRepositoryTransactional ??= new ProductRepositoryTransactional(_context);
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}