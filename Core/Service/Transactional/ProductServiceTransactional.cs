using Domain.Entities;
using Domain.Interfaces.Services.Transactional;
using Domain.Interfaces.UnitOfWork.Transactional;

namespace Core.Service.Transactional
{
    public class ProductServiceTransactional : IProductServiceTransactional
    {
        private readonly IProductUnitOfWorkTransactional _unitOfWork;

        public ProductServiceTransactional(IProductUnitOfWorkTransactional unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Inserta un nuevo producto.
        /// </summary>
        /// <param name="product">El producto a insertar.</param>
        /// <returns>El producto insertado.</returns>
        public async Task<Product> InsertAsync(Product product)
        {
            var insertedProduct = await _unitOfWork.ProductRepositoryTransactional.InsertAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return insertedProduct;
        }

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="product">El producto con los datos actualizados.</param>
        /// <returns>El producto actualizado.</returns>
        public async Task<Product> UpdateAsync(Product product)
        {
            var updatedProduct = await _unitOfWork.ProductRepositoryTransactional.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return updatedProduct;
        }

        /// <summary>
        /// Inactiva un producto por su ProductSeq.
        /// </summary>
        /// <param name="productSeq">El identificador único del producto.</param>
        /// <returns>Un valor booleano indicando si la operación fue exitosa.</returns>
        public async Task<bool> InactivateAsync(int productSeq)
        {
            var result = await _unitOfWork.ProductRepositoryTransactional.InactivateAsync(productSeq);
            if (result)
            {
                await _unitOfWork.SaveChangesAsync();
            }
            return result;
        }
    }
}