using Domain.Interfaces.Repositories.Transactional;

namespace Domain.Interfaces.UnitOfWork.Transactional
{
    public interface IProductUnitOfWorkTransactional : IDisposable
    {
        IProductRepositoryTransactional ProductRepositoryTransactional { get; }
        Task<int> SaveChangesAsync();
    }
}