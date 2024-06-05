using _3.Data.Context;
using _3.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace _3.Data
{
    public class ProductMySqlData : IProductData
    {
        private ArtisaniaDBContext _artisaniaDbContext;

        public ProductMySqlData(ArtisaniaDBContext artisaniaDbContext)
        {
            _artisaniaDbContext = artisaniaDbContext;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _artisaniaDbContext.Products.Where(p => p.IsActive == true).ToListAsync();
        }

        public async Task<bool> SaveAsync(Product data)
        {
            data.IsActive = true;
            using (var transaction = await _artisaniaDbContext.Database.BeginTransactionAsync())
            {
                _artisaniaDbContext.Products.Add(data);
                await _artisaniaDbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return true;
        }

        public async Task<bool> UpdateAsync(Product data, int id)
        {
            using (var transaction = await _artisaniaDbContext.Database.BeginTransactionAsync())
            {
                var productToUpdate = _artisaniaDbContext.Products.Where(t => t.Id == id).FirstOrDefault();
                productToUpdate.Name = data.Name;
                productToUpdate.Unit_Price = data.Unit_Price;
                productToUpdate.Stock = data.Stock;
                productToUpdate.Description = data.Description;//
                productToUpdate.Category = data.Category;//
                productToUpdate.Image = data.Image;//
                _artisaniaDbContext.Products.Update(productToUpdate);
                await _artisaniaDbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var transaction = await _artisaniaDbContext.Database.BeginTransactionAsync())
            {
                var productToDelete = _artisaniaDbContext.Products.Where(t => t.Id == id).FirstOrDefault();
                productToDelete.IsActive = false;
                await _artisaniaDbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return true;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _artisaniaDbContext.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
        }
    }
}