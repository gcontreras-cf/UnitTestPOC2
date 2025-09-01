using Domain.Interfaces.Repositories.Transactional;
using Domain.Interfaces.UnitOfWork.Transactional;
using Infraestructure.Data;
using Infraestructure.Repositories.Transactional;

namespace Infraestructure.UnitOfWork.Transactional
{
    public class OrderUnitOfWorkTransactional : IOrderUnitOfWorkTransactional
    {
        private readonly AppDbContext _context;
        private IOrderRepositoryTransactional? _orderRepositoryTransactional;

        public OrderUnitOfWorkTransactional(AppDbContext context)
        {
            _context = context;
        }

        public IOrderRepositoryTransactional OrderRepositoryTransactional =>
            _orderRepositoryTransactional ??= new OrderRepositoryTransactional(_context);

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