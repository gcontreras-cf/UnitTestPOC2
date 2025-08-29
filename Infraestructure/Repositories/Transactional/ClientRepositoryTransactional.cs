using Domain.Entities;
using Domain.Interfaces.Repositories.Transactional;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories.Transactional
{
    public class ClientRepositoryTransactional : IClientRepositoryTransactional
    {
        private readonly AppDbContext _context;

        public ClientRepositoryTransactional(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Inserta un nuevo cliente.
        /// </summary>
        /// <param name="client">El cliente a insertar.</param>
        /// <returns>El cliente insertado.</returns>
        public async Task<Client> InsertAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        /// <summary>
        /// Actualiza un cliente existente.
        /// </summary>
        /// <param name="client">El cliente con los datos actualizados.</param>
        /// <returns>El cliente actualizado.</returns>
        public async Task<Client> UpdateAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
            return client;
        }

        /// <summary>
        /// Inactiva un cliente por su ClientSeq.
        /// </summary>
        /// <param name="clientSeq">El identificador único del cliente.</param>
        /// <returns>Un valor booleano indicando si la operación fue exitosa.</returns>
        public async Task<bool> InactivateAsync(int clientSeq)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.ClientSeq == clientSeq);
            if (client == null)
            {
                return false;
            }

            client.Active = false;
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}