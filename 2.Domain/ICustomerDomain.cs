using _3.Data.Models;
namespace _2.Domain;

public interface ICustomerDomain
{
    Task<Boolean> SaveAsync(Customer data);
    Task<Boolean> UpdateAsync(Customer data, int id);
    Task<Boolean> DeleteAsync(int id);
}