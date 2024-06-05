using System.Text.RegularExpressions;
using _3.Data;
using _3.Data.Models;

namespace _2.Domain.ArtisanDomain;

public class ArtisanDomain : IArtisanDomain
{
    private IArtisanData _artisanData;
    public ArtisanDomain(IArtisanData artisanData)
    {
        _artisanData = artisanData;
    }
    public async Task<Boolean> SaveAsync(Artisan data)
    {
        var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        
        if (!emailRegex.IsMatch(data.Email))
        {
            throw new Exception("El correo electrónico no tiene un formato válido");
        }
        
        if (data.Name.Length < 2 || data.Name.Length > 50)
        {
            throw new ArgumentException("El nombre debe tener entre 2 y 50 caracteres", nameof(data.Name));
        }
        
        if (data.LastName.Length < 2 || data.LastName.Length > 50)
        {
            throw new ArgumentException("El apellido debe tener entre 2 y 50 caracteres", nameof(data.LastName));
        }
        
        
        var existingArtisan = await _artisanData.GetByEmail(data.Email);
        if(existingArtisan != null)
        {
            throw new Exception("El correo electrónico ya está registrado");
        }
        
        var existingArtisanByPhone = await _artisanData.GetByPhone(data.Phone);
        if (existingArtisanByPhone != null)
        {
            throw new Exception("El número de teléfono ya está registrado");
        }
        
        return await _artisanData.SaveAsync(data);
    }
    
    public async Task<Boolean> UpdateAsync(Artisan data, int id)
    {
        return await _artisanData.UpdateAsync(data, id);
    }
    
    public async Task<Boolean> DeleteAsync(int id)
    {
        return await _artisanData.DeleteAsync(id);
    }
}