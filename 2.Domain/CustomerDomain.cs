using System.Runtime.InteropServices.JavaScript;
using _3.Data;
using _3.Data.Models;

namespace _2.Domain;

public class CustomerDomain: ICustomerDomain
{
    private ICustomerData _customerData;
    public CustomerDomain(ICustomerData customerData)
    {
        _customerData = customerData;
    }
    public async Task<Boolean> SaveAsync(Customer data)
    {
        var existingCustomer = await _customerData.GetByEmail(data.Email);
        if(existingCustomer != null)
        {
            throw new Exception("Correo vinculado con una cuenta existente");
        }
        return await _customerData.SaveAsync(data);
    }
    
    public async Task<Boolean> UpdateAsync(Customer data, int id)
    {
        return await _customerData.UpdateAsync(data, id);
    }
    
    public async Task<Boolean> DeleteAsync(int id)
    {
        return await _customerData.DeleteAsync(id);
    }
}