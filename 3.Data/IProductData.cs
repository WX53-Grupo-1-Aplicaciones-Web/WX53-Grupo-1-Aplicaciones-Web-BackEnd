using _3.Data.Models;

namespace _3.Data
{
    public interface IProductData
    {
        Task<bool> SaveAsync(Product data);
        Task<bool> UpdateAsync(Product data, int id);
        Task<bool> DeleteAsync(int id);

        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
    }
}