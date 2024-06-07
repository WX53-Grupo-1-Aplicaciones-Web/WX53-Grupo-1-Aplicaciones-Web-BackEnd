using _3.Data.Context;
using _3.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace _3.Data;

public class OrderMySqlData: IOrderData
{
    private ArtisaniaDBContext _artisaniaDbContext;

    public OrderMySqlData(ArtisaniaDBContext artisaniaDbContext)
    {
        _artisaniaDbContext = artisaniaDbContext;
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _artisaniaDbContext.Orders.Where(o => o.IsActive == true).ToListAsync();
    }

    public async Task<Boolean> SaveAsync(Order data)
    {
        data.IsActive = true;
        using (var transaction = await _artisaniaDbContext.Database.BeginTransactionAsync())
        {
            _artisaniaDbContext.Orders.Add(data);
            await _artisaniaDbContext.SaveChangesAsync();
            transaction.Commit();
        }

        return true;
    }

    public async Task<Boolean> UpdateAsync(Order data, int id)
    {
        using (var transaction = await _artisaniaDbContext.Database.BeginTransactionAsync())
        {
            var OrderToUpdate = _artisaniaDbContext.Orders.Where(o => o.Id == id).FirstOrDefault();
            OrderToUpdate.RequestDate = data.RequestDate;
            OrderToUpdate.ShippingDate = data.ShippingDate;
            OrderToUpdate.DeliveryAddress = data.DeliveryAddress;
            OrderToUpdate.Status = data.Status;
            _artisaniaDbContext.Orders.Update(OrderToUpdate);
            await _artisaniaDbContext.SaveChangesAsync();
            transaction.Commit();
        }

        return true;
    }

    public async Task<Boolean> DeleteAsync(int id)
    {
        using (var transaction = await _artisaniaDbContext.Database.BeginTransactionAsync())
        {
            var OrderToDelete = _artisaniaDbContext.Customers.Where(o => o.Id == id).FirstOrDefault();
            OrderToDelete.IsActive = false;
            await _artisaniaDbContext.SaveChangesAsync();
            transaction.Commit();
        }

        return true;
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        return await _artisaniaDbContext.Orders.Where(o => o.Id == id).FirstOrDefaultAsync();
    }
}