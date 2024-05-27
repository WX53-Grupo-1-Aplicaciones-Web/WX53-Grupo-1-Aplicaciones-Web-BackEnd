using System.Runtime.InteropServices.JavaScript;
using _3.Data.Models;

namespace _3.Data;

public interface ICustomerData
{
    Task<Boolean> SaveAsync(Customer data);
    Task<Boolean> UpdateAsync(Customer data, int id);
    Task<Boolean> DeleteAsync(int id);
    
    Task<List<Customer>> GetAllAsync();
    Task<Customer> GetByIdAsync(int id);
    Task<Customer>GetByEmail(string email);

}