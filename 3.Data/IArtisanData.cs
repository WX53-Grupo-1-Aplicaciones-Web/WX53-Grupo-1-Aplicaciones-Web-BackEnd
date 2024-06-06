using _3.Data.Models;

namespace _3.Data;

public interface IArtisanData
{
    Task<Boolean> SaveAsync(Artisan data);
    Task<Boolean> UpdateAsync(Artisan data, int id);
    Task<Boolean> DeleteAsync(int id);
    
    Task<List<Artisan>> GetAllAsync();
    Task<Artisan> GetByIdAsync(int id);
    Task<Artisan>GetByEmail(string email);
    Task<Artisan> GetByPhone(long phone);
}