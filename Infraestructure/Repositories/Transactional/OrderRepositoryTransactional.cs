using Domain.Entities;
using Domain.Interfaces.Repositories.Transactional;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories.Transactional
{
    public class OrderRepositoryTransactional : IOrderRepositoryTransactional
    {
        private readonly AppDbContext _context;

        public OrderRepositoryTransactional(AppDbContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByDateAsync(DateTime date)
        {
            return await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .Where(o => o.OrderDate.Date == date && o.Active)
                .ToListAsync();
        }
    }
}