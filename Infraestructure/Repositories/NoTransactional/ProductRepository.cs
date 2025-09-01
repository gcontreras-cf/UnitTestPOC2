using Domain.Entities;
using Domain.Interfaces.Repositories.NoTransactional;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories.NoTransactional
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Where(p => p.Active)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByNameAsync(string name)
        {
            return await _context.Products
                .Where(p => p.Active && p.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<Product?> GetByProductSeqAsync(int productSeq)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Active && p.ProductSeq == productSeq);
        }
    }
}