using _3.Data;
using _3.Data.Models;

namespace _2.Domain.ProductDomain;
public class ProductDomain : IProductDomain
{
    private IProductData _productData;

        public ProductDomain(IProductData productData)
        {
            _productData = productData;
        }

        public async Task<bool> SaveAsync(Product data)
        {
            return await _productData.SaveAsync(data);
        }

        public async Task<bool> UpdateAsync(Product data, int id)
        {
            return await _productData.UpdateAsync(data, id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _productData.DeleteAsync(id);
        }
}
