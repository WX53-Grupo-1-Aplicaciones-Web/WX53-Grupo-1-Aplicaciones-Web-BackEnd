using System.Runtime.InteropServices.JavaScript;
using _3.Data.Context;
using _3.Data.Models;
using Microsoft.EntityFrameworkCore;
namespace _3.Data;

public class CustomerMySqlData:ICustomerData
{
    private ArtisaniaDBContext _artisaniaDbContext;
    
    public CustomerMySqlData(ArtisaniaDBContext artisaniaDbContext)
    {
        _artisaniaDbContext = artisaniaDbContext;
    }
    
    public async Task<List<Customer>> GetAllAsync()
    {
        return await _artisaniaDbContext.Customers.Where(c=> c.IsActive == true).ToListAsync();
    }
    
    public async Task<Boolean> SaveAsync(Customer data)
    {
        data.IsActive = true;
        using (var transaction = await _artisaniaDbContext.Database.BeginTransactionAsync())
        {
            _artisaniaDbContext.Customers.Add(data);
            await _artisaniaDbContext.SaveChangesAsync();
            transaction.Commit();

        }
        return true;
    }
    
    public async Task<Boolean> UpdateAsync(Customer data, int id)
    {
        
        using (var transaction = await _artisaniaDbContext.Database.BeginTransactionAsync())
        {
            var customerToUpdate =  _artisaniaDbContext.Customers.Where(t => t.Id == id).FirstOrDefault();
            customerToUpdate.Name = data.Name;
            customerToUpdate.LastName = data.LastName;
            customerToUpdate.Phone = data.Phone;
            _artisaniaDbContext.Customers.Update(customerToUpdate);
            await _artisaniaDbContext.SaveChangesAsync();
            transaction.Commit();
        }
        return true;
    }
    
    public async Task<Boolean> DeleteAsync(int id)
    {
        using (var transaction = await _artisaniaDbContext.Database.BeginTransactionAsync())
        {
            var customerToDelete = _artisaniaDbContext.Customers.Where(t => t.Id == id).FirstOrDefault();
            customerToDelete.IsActive = false;
            await _artisaniaDbContext.SaveChangesAsync();
            transaction.Commit();
        }
        return true;
    }
    
    public async Task<Customer> GetByIdAsync(int id)
    {
        return await _artisaniaDbContext.Customers.Where(c => c.Id == id).FirstOrDefaultAsync();
    }
    
    public async Task<Customer> GetByEmail(string email)
    {
        return await _artisaniaDbContext.Customers.Where(c => c.Email == email).FirstOrDefaultAsync();
    }

}