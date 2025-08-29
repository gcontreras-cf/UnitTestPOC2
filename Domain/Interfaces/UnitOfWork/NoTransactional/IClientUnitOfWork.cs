using Domain.Interfaces.Repositories.NoTransactional;

namespace Domain.Interfaces.UnitOfWork.NoTransactional
{
    public interface IClientUnitOfWork : IDisposable
    {
        /// <summary>
        /// Repositorio para acceder a los datos de clientes.
        /// </summary>
        IClientRepository ClientRepository { get; }
    }
}