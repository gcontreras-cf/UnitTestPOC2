using Domain.Entities;

namespace Domain.Interfaces.Repositories.Transactional
{
    public interface IProductRepositoryTransactional
    {
        Task<Product> InsertAsync(Product product);
        Task<Product> UpdateAsync(Product product);
        Task<bool> InactivateAsync(int productSeq);
    }
}