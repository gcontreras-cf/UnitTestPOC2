using Domain.Entities;
using Domain.Interfaces.Services.NoTransactional;
using Domain.Interfaces.UnitOfWork.NoTransactional;

namespace Core.Service.NoTransactional
{
    public class ProductService : IProductService
    {
        private readonly IProductUnitOfWork _unitOfWork;

        public ProductService(IProductUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Obtiene todos los productos activos.
        /// </summary>
        /// <returns>Una lista de productos activos.</returns>
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _unitOfWork.ProductRepository.GetAllAsync();
        }

        /// <summary>
        /// Filtra los productos activos por nombre.
        /// </summary>
        /// <param name="name">El nombre del producto a buscar.</param>
        /// <returns>Una lista de productos activos que coinciden con el nombre.</returns>
        public async Task<IEnumerable<Product>> GetByNameAsync(string name)
        {
            return await _unitOfWork.ProductRepository.GetByNameAsync(name);
        }

        /// <summary>
        /// Obtiene un producto activo por su ProductSeq.
        /// </summary>
        /// <param name="productSeq">El identificador único del producto.</param>
        /// <returns>El producto activo correspondiente o null si no se encuentra.</returns>
        public async Task<Product?> GetByProductSeqAsync(int productSeq)
        {
            return await _unitOfWork.ProductRepository.GetByProductSeqAsync(productSeq);
        }
    }
}