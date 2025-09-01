using Domain.Entities;

namespace Domain.Interfaces.Repositories.NoTransactional
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetByNameAsync(string name);
        Task<Product?> GetByProductSeqAsync(int productSeq);
    }
}