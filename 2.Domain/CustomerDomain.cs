using System.Runtime.InteropServices.JavaScript;
using _3.Data;
using _3.Data.Models;
using _4.Shared;
using System.Text.RegularExpressions;

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
        var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

        if (!emailRegex.IsMatch(data.Email))
        {
            throw new Exception("El correo electrónico no tiene un formato válido");
        }

        if (!Regex.IsMatch(data.Phone.ToString(), CONSTANTS.PHONE_PATTERN))
        {
            throw new ArgumentException("El número de teléfono no tiene un formato válido", nameof(data.Phone));
        }
        
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