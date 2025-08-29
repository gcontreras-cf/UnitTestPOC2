using Domain.Interfaces.Repositories.Transactional;
using Domain.Interfaces.UnitOfWork.Transactional;
using Infraestructure.Data;
using Infraestructure.Repositories.Transactional;

namespace Infraestructure.UnitOfWork.Transactional
{
    public class ClientUnitOfWorkTransactional : IClientUnitOfWorkTransactional
    {
        private readonly AppDbContext _context;
        private IClientRepositoryTransactional? _clientRepositoryTransactional;

        public ClientUnitOfWorkTransactional(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Repositorio para operaciones transaccionales de clientes.
        /// </summary>
        public IClientRepositoryTransactional ClientRepositoryTransactional
        {
            get
            {
                return _clientRepositoryTransactional ??= new ClientRepositoryTransactional(_context);
            }
        }

        /// <summary>
        /// Guarda los cambios realizados en el contexto actual.
        /// </summary>
        /// <returns>El número de cambios realizados.</returns>
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Libera los recursos utilizados por el contexto.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}