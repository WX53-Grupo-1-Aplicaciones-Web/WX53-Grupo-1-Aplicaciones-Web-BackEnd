using _3.Data.Models;

namespace _2.Domain.OrderDomain;

public interface IOrderDomain
{
    Task<Boolean> SaveAsync(Order data);
    Task<Boolean> UpdateAsync(Order data, int id);
    Task<Boolean> DeleteAsync(int id);
}