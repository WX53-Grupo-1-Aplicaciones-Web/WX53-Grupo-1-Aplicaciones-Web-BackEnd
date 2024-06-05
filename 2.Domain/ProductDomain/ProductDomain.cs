using _3.Data;
using _3.Data.Models;
using _4.Shared;

using System.Text.RegularExpressions;


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
            // Validar longitud del nombre del producto 
            if (data.Name.Length > 50)
            {
                throw new ArgumentException("El nombre del producto no puede tener más de 50 caracteres.");
            }
            
            // Validar precio unitario
            if (data.Unit_Price <= 0)
            {
                throw new ArgumentException("El precio unitario del producto debe ser positivo.");
            }
            
            // Validar formato de precio unitario (opcional)
            if (data.Unit_Price > 0 && !Regex.IsMatch(data.Unit_Price.ToString(), @"^\d+(\.\d{1,2})?$"))
            {
                throw new ArgumentException("El precio unitario del producto debe tener un formato decimal válido (hasta dos decimales).");
            }
            
            // Validar stock
            if (data.Stock <= 0)
            {
                throw new ArgumentException("El stock del producto no puede ser negativo.");
            }
            
            // Validar longitud de la descripción del producto
            if (data.Description.Length > 300)
            {
                throw new ArgumentException("La descripción del producto no puede tener más de 300 caracteres.");
            }
            
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
