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
        var existingArtisan = await _artisanData.GetByEmail(data.Email);
        if(existingArtisan != null)
        {
            throw new Exception("Correo vinculado con una cuenta existente");
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