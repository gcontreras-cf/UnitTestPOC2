using Domain.Entities;

namespace Domain.Interfaces.Services.NoTransactional
{
    public interface IClientService
    {
        /// <summary>
        /// Obtiene todos los registros de clientes.
        /// </summary>
        /// <returns>Una lista de clientes.</returns>
        Task<IEnumerable<Client>> GetAllAsync();

        /// <summary>
        /// Filtra los clientes por nombre.
        /// </summary>
        /// <param name="name">El nombre del cliente a buscar.</param>
        /// <returns>Una lista de clientes que coinciden con el nombre.</returns>
        Task<IEnumerable<Client>> GetByNameAsync(string name);

        /// <summary>
        /// Obtiene un cliente por su ClientSeq.
        /// </summary>
        /// <param name="clientSeq">El identificador único del cliente.</param>
        /// <returns>El cliente correspondiente o null si no se encuentra.</returns>
        Task<Client?> GetByClientSeqAsync(int clientSeq);
    }
}