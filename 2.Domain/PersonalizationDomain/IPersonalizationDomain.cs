using _3.Data.Models;

namespace _2.Domain.PersonalizationDomain;

public interface IPersonalizationDomain
{
    Task<bool> SaveAsync(Personalization data);
    Task<bool> UpdateAsync(Personalization data, int id);
    Task<bool> DeleteAsync(int id);

}