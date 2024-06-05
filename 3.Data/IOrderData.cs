using _3.Data.Models;

namespace _3.Data;

public interface IOrderData
{
    Task<Boolean> SaveAsync(Order data);
    Task<Boolean> UpdateAsync(Order data, int id);
    Task<Boolean> DeleteAsync(int id);
    
    Task<List<Order>> GetAllAsync();
    Task<Order> GetByIdAsync(int id);
}