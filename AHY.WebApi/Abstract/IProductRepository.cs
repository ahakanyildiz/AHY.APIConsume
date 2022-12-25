using AHY.WebApi.Data;

namespace AHY.WebApi.Abstract
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetById(int id);
        Task<Product> Create(Product product);
        void Remove(int id);
        Task<bool> Update(Product product);
    }
}
