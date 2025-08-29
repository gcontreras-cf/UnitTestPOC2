using Domain.Entities;
using Domain.Interfaces.Services.Transactional;
using Domain.Interfaces.UnitOfWork.Transactional;

namespace Core.Service.Transactional
{
    public class ClientServiceTransactional : IClientServiceTransactional
    {
        private readonly IClientUnitOfWorkTransactional _unitOfWork;

        public ClientServiceTransactional(IClientUnitOfWorkTransactional unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Inserta un nuevo cliente.
        /// </summary>
        /// <param name="client">El cliente a insertar.</param>
        /// <returns>El cliente insertado.</returns>
        public async Task<Client> InsertAsync(Client client)
        {
            var insertedClient = await _unitOfWork.ClientRepositoryTransactional.InsertAsync(client);
            await _unitOfWork.SaveChangesAsync();
            return insertedClient;
        }

        /// <summary>
        /// Actualiza un cliente existente.
        /// </summary>
        /// <param name="client">El cliente con los datos actualizados.</param>
        /// <returns>El cliente actualizado.</returns>
        public async Task<Client> UpdateAsync(Client client)
        {
            var updatedClient = await _unitOfWork.ClientRepositoryTransactional.UpdateAsync(client);
            await _unitOfWork.SaveChangesAsync();
            return updatedClient;
        }

        /// <summary>
        /// Inactiva un cliente por su ClientSeq.
        /// </summary>
        /// <param name="clientSeq">El identificador único del cliente.</param>
        /// <returns>Un valor booleano indicando si la operación fue exitosa.</returns>
        public async Task<bool> InactivateAsync(int clientSeq)
        {
            var result = await _unitOfWork.ClientRepositoryTransactional.InactivateAsync(clientSeq);
            if (result)
            {
                await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }
    }
}