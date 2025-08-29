using Domain.Entities;
using Domain.Interfaces.Repositories.NoTransactional;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories.NoTransactional
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los registros de clientes activos.
        /// </summary>
        /// <returns>Una lista de clientes activos.</returns>
        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Clients
                .Where(c => c.Active)
                .ToListAsync();
        }

        /// <summary>
        /// Filtra los clientes activos por nombre.
        /// </summary>
        /// <param name="name">El nombre del cliente a buscar.</param>
        /// <returns>Una lista de clientes activos que coinciden con el nombre.</returns>
        public async Task<IEnumerable<Client>> GetByNameAsync(string name)
        {
            return await _context.Clients
                .Where(c => c.Active && c.Name.Contains(name))
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene un cliente activo por su ClientSeq.
        /// </summary>
        /// <param name="clientSeq">El identificador único del cliente.</param>
        /// <returns>El cliente activo correspondiente o null si no se encuentra.</returns>
        public async Task<Client?> GetByClientSeqAsync(int clientSeq)
        {
            return await _context.Clients
                .FirstOrDefaultAsync(c => c.Active && c.ClientSeq == clientSeq);
        }
    }
}