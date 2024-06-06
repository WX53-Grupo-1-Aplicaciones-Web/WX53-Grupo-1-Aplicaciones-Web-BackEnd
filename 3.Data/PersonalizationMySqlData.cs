using _3.Data.Context;
using _3.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace _3.Data;

public class PersonalizationMySQLData : IPersonalizationData
{
    private ArtisaniaDBContext _artisaniaDbContext;

    public PersonalizationMySQLData(ArtisaniaDBContext artisaniaDbContext)
    {
        _artisaniaDbContext = artisaniaDbContext;
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Personalization>> GetAllAsync()
    {
        return await _artisaniaDbContext.Personalizations.Where(p => p.IsActive == true).ToListAsync();
    }

    public async Task<Personalization> GetByIdAsync(int id)
    {
        return await _artisaniaDbContext.Personalizations.Where(p => p.Id == id).FirstOrDefaultAsync();
    }

    public Task<bool> SaveAsync(Personalization data)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Personalization data, int id)
    {
        throw new NotImplementedException();
    }
}