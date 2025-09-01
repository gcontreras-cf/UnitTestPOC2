using Domain.Entities;
using Domain.Interfaces.Repositories.Transactional;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories.Transactional
{
    public class ProductRepositoryTransactional : IProductRepositoryTransactional
    {
        private readonly AppDbContext _context;

        public ProductRepositoryTransactional(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> InsertAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> InactivateAsync(int productSeq)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductSeq == productSeq);
            if (product == null)
            {
                return false;
            }

            product.Active = false;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}