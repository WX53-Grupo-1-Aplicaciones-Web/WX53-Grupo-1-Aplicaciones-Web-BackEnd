using _3.Data.Models;

namespace _3.Data;

public interface IPersonalizationData
{
    Task<bool> SaveAsync(Personalization data);
    Task<bool> UpdateAsync(Personalization data, int id);
    Task<bool> DeleteAsync(int id);
    Task<List<Personalization>> GetAllAsync();
    Task<Personalization> GetByIdAsync(int id);
}