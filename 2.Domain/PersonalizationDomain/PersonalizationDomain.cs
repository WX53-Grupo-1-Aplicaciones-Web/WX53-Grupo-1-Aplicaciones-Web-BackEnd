using _3.Data;
using _3.Data.Models;

namespace _2.Domain.PersonalizationDomain;

public class PersonalizationDomain : IPersonalizationDomain
{
    private IPersonalizationData _personalizationData;

    public PersonalizationDomain(IPersonalizationData personalizationData)
    {
        _personalizationData = personalizationData;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _personalizationData.DeleteAsync(id);
    }

    public async Task<bool> SaveAsync(Personalization data)
    {
        return await _personalizationData.SaveAsync(data);
    }

    public async Task<bool> UpdateAsync(Personalization data, int id)
    {
        return await _personalizationData.UpdateAsync(data, id);
    }
}