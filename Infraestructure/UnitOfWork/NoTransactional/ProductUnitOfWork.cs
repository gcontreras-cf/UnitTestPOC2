using Domain.Interfaces.Repositories.NoTransactional;
using Domain.Interfaces.UnitOfWork.NoTransactional;
using Infraestructure.Data;
using Infraestructure.Repositories.NoTransactional;

namespace Infraestructure.UnitOfWork.NoTransactional
{
    public class ProductUnitOfWork : IProductUnitOfWork
    {
        private readonly AppDbContext _context;
        private IProductRepository? _productRepository;

        public ProductUnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IProductRepository ProductRepository
        {
            get
            {
                return _productRepository ??= new ProductRepository(_context);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}