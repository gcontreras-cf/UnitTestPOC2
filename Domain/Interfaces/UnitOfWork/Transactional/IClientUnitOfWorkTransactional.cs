using Domain.Interfaces.Repositories.Transactional;

namespace Domain.Interfaces.UnitOfWork.Transactional
{
    public interface IClientUnitOfWorkTransactional : IDisposable
    {
        /// <summary>
        /// Repositorio para operaciones transaccionales de clientes.
        /// </summary>
        IClientRepositoryTransactional ClientRepositoryTransactional { get; }

        /// <summary>
        /// Guarda los cambios realizados en el contexto actual.
        /// </summary>
        /// <returns>El número de cambios realizados.</returns>
        Task<int> SaveChangesAsync();
    }
}