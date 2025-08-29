using Domain.Interfaces.Repositories.NoTransactional;
using Domain.Interfaces.UnitOfWork.NoTransactional;
using Infraestructure.Data;
using Infraestructure.Repositories.NoTransactional;

namespace Infraestructure.UnitOfWork.NoTransactional
{
    public class ClientUnitOfWork : IClientUnitOfWork
    {
        private readonly AppDbContext _context;
        private IClientRepository? _clientRepository;

        public ClientUnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Repositorio para acceder a los datos de clientes.
        /// </summary>
        public IClientRepository ClientRepository
        {
            get
            {
                return _clientRepository ??= new ClientRepository(_context);
            }
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