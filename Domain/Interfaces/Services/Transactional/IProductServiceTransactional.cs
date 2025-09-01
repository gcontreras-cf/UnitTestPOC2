using Domain.Entities;

namespace Domain.Interfaces.Services.Transactional
{
    public interface IProductServiceTransactional
    {
        Task<Product> InsertAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<bool> InactivateAsync(int productSeq);
    }
}