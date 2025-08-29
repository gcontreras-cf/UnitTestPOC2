using Domain.Entities;

namespace Domain.Interfaces.Repositories.Transactional
{
    public interface IClientRepositoryTransactional
    {
        /// <summary>
        /// Inserta un nuevo cliente.
        /// </summary>
        /// <param name="client">El cliente a insertar.</param>
        /// <returns>El cliente insertado.</returns>
        Task<Client> InsertAsync(Client client);

        /// <summary>
        /// Actualiza un cliente existente.
        /// </summary>
        /// <param name="client">El cliente con los datos actualizados.</param>
        /// <returns>El cliente actualizado.</returns>
        Task<Client> UpdateAsync(Client client);

        /// <summary>
        /// Inactiva un cliente por su ClientSeq.
        /// </summary>
        /// <param name="clientSeq">El identificador único del cliente.</param>
        /// <returns>Un valor booleano indicando si la operación fue exitosa.</returns>
        Task<bool> InactivateAsync(int clientSeq);
    }
}