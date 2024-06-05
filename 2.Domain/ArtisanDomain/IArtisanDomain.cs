using _3.Data.Models;

namespace _2.Domain.ArtisanDomain;

public interface IArtisanDomain
{
    Task<Boolean> SaveAsync(Artisan data);
    Task<Boolean> UpdateAsync(Artisan data, int id);
    Task<Boolean> DeleteAsync(int id);
}