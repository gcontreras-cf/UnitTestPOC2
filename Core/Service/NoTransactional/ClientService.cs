using Domain.Entities;
using Domain.Interfaces.Services.NoTransactional;
using Domain.Interfaces.UnitOfWork.NoTransactional;

namespace Core.Service.NoTransactional
{
    public class ClientService : IClientService
    {
        private readonly IClientUnitOfWork _unitOfWork;

        public ClientService(IClientUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Obtiene todos los registros de clientes.
        /// </summary>
        /// <returns>Una lista de clientes.</returns>
        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _unitOfWork.ClientRepository.GetAllAsync();
        }

        /// <summary>
        /// Filtra los clientes por nombre.
        /// </summary>
        /// <param name="name">El nombre del cliente a buscar.</param>
        /// <returns>Una lista de clientes que coinciden con el nombre.</returns>
        public async Task<IEnumerable<Client>> GetByNameAsync(string name)
        {
            return await _unitOfWork.ClientRepository.GetByNameAsync(name);
        }

        /// <summary>
        /// Obtiene un cliente por su ClientSeq.
        /// </summary>
        /// <param name="clientSeq">El identificador único del cliente.</param>
        /// <returns>El cliente correspondiente o null si no se encuentra.</returns>
        public async Task<Client?> GetByClientSeqAsync(int clientSeq)
        {
            return await _unitOfWork.ClientRepository.GetByClientSeqAsync(clientSeq);
        }
    }
}