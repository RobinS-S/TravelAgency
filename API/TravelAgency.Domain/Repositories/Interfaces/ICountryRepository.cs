using TravelAgency.Domain.Entities;

namespace TravelAgency.Domain.Repositories.Interfaces
{
    public interface ICountryRepository
    {
        Task AddAsync(Country country);
        Task DeleteAsync(long id);
        Task<IEnumerable<Country>> GetAllAsync();
        Task<Country?> GetByIdAsync(long id);
        Task UpdateAsync(Country country);
    }
}