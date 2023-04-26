using TravelAgency.Domain.Entities;

namespace TravelAgency.Domain.Repositories.Interfaces
{
    public interface ILocationRepository
    {
        Task AddAsync(Location location);
        Task DeleteAsync(long id);
        Task<IEnumerable<Location>> GetAllAsync();
        Task<Location?> GetByIdAsync(long id);
        Task UpdateAsync(Location location);
    }
}