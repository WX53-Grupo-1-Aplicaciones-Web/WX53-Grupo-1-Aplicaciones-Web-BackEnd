using _3.Data.Context;
using _3.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace _3.Data;

public class ArtisanMySqlData: IArtisanData
{
    private ArtisaniaDBContext _artisaniaDbContext;
    
    public ArtisanMySqlData(ArtisaniaDBContext artisaniaDbContext)
    {
        _artisaniaDbContext = artisaniaDbContext;
    }
    
    public async Task<List<Artisan>> GetAllAsync()
    {
        return await _artisaniaDbContext.Artisans.Where(c=> c.IsActive == true).ToListAsync();
    }
    
    public async Task<Boolean> SaveAsync(Artisan data)
    {
        data.IsActive = true;
        using (var transaction = await _artisaniaDbContext.Database.BeginTransactionAsync())
        {
            _artisaniaDbContext.Artisans.Add(data);
            await _artisaniaDbContext.SaveChangesAsync();
            transaction.Commit();

        }
        return true;
    }
    
    public async Task<Boolean> UpdateAsync(Artisan data, int id)
    {
        
        using (var transaction = await _artisaniaDbContext.Database.BeginTransactionAsync())
        {
            var artisanToUpdate =  _artisaniaDbContext.Artisans.Where(t => t.Id == id).FirstOrDefault();
            artisanToUpdate.Name = data.Name;
            artisanToUpdate.LastName = data.LastName;
            artisanToUpdate.Phone = data.Phone;
            _artisaniaDbContext.Artisans.Update(artisanToUpdate);
            await _artisaniaDbContext.SaveChangesAsync();
            transaction.Commit();
        }
        return true;
    }
    
    public async Task<Boolean> DeleteAsync(int id)
    {
        using (var transaction = await _artisaniaDbContext.Database.BeginTransactionAsync())
        {
            var artisanToDelete = _artisaniaDbContext.Artisans.Where(t => t.Id == id).FirstOrDefault();
            artisanToDelete.IsActive = false;
            await _artisaniaDbContext.SaveChangesAsync();
            transaction.Commit();
        }
        return true;
    }
    
    public async Task<Artisan> GetByIdAsync(int id)
    {
        return await _artisaniaDbContext.Artisans.Where(c => c.Id == id).FirstOrDefaultAsync();
    }
    
    public async Task<Artisan> GetByEmail(string email)
    {
        return await _artisaniaDbContext.Artisans.Where(c => c.Email == email).FirstOrDefaultAsync();
    }
}