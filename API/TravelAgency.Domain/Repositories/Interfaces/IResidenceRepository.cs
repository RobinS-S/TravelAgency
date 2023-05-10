using TravelAgency.Domain.Entities;

namespace TravelAgency.Domain.Repositories.Interfaces
{
    public interface IResidenceRepository
    {
        Task AddAsync(Residence residence);
        Task DeleteAsync(long id);
        Task<IEnumerable<Residence>> GetAllAsync();
        Task<Residence?> GetByIdAsync(long id);
        Task UpdateAsync(Residence residence);
    }
}