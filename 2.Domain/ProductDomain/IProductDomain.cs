using _3.Data.Models;
namespace _2.Domain.ProductDomain

{
    public interface IProductDomain
    {
        Task<bool> SaveAsync(Product data);
        Task<bool> UpdateAsync(Product data, int id);
        Task<bool> DeleteAsync(int id);
    }
}