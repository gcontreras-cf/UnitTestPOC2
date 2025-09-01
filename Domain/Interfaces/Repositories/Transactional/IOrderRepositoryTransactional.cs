using Domain.Entities;

namespace Domain.Interfaces.Repositories.Transactional
{
    public interface IOrderRepositoryTransactional
    {
        Task InsertAsync(Order order);
        Task<IEnumerable<Order>> GetOrdersByDateAsync(DateTime date);
    }
}