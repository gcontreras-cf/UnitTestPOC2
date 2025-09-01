using Domain.Interfaces.Repositories.NoTransactional;

namespace Domain.Interfaces.UnitOfWork.NoTransactional
{
    public interface IProductUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
    }
}