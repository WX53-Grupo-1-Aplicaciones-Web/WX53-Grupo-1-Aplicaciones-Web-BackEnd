using _3.Data;
using _3.Data.Models;

namespace _2.Domain.OrderDomain;

public class OrderDomain: IOrderDomain
{
    private IOrderData _orderData;

    public OrderDomain(IOrderData orderData)
    {
        _orderData = orderData;
    }

    public async Task<bool> SaveAsync(Order data)
    {
        return await _orderData.SaveAsync(data);
    }

    public async Task<bool> UpdateAsync(Order data, int id)
    {
        return await _orderData.UpdateAsync(data, id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _orderData.DeleteAsync(id);
    }
}