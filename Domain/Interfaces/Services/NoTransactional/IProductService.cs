using Domain.Entities;

namespace Domain.Interfaces.Services.NoTransactional
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetByNameAsync(string name);
        Task<Product?> GetByProductSeqAsync(int productSeq);
    }
}