using Domain.Interfaces.Repositories.Transactional;

namespace Domain.Interfaces.UnitOfWork.Transactional
{
    public interface IOrderUnitOfWorkTransactional : IDisposable
    {
        IOrderRepositoryTransactional OrderRepositoryTransactional { get; }
        Task<int> SaveChangesAsync();
    }
}