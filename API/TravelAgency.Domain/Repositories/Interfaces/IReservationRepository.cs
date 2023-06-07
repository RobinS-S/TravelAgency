using TravelAgency.Domain.Entities;

namespace TravelAgency.Domain.Repositories.Interfaces
{
    public interface IReservationRepository
    {
        Task AddAsync(Reservation reservation);
        Task DeleteAsync(long id);
        Task<IEnumerable<Reservation>> GetAllAsync();
        Task<IEnumerable<Reservation>> GetAllByResidenceIdAndBetweenAsync(long residenceId, DateTime start, DateTime end);
        Task<IEnumerable<Reservation>> GetAllByResidenceIdAsync(long residenceId);
        Task<IEnumerable<Reservation>> GetAllByTenantIdAsync(string userId);
        Task<IEnumerable<Reservation>> GetAllByOwnerIdAsync(string ownerId);
        Task<Reservation?> GetActiveByTenantIdAsync(string tenantId);
        Task<IEnumerable<Reservation>> GetActiveByOwnerIdAsync(string ownerId);
        Task<Reservation?> GetByIdAsync(long id);
        Task UpdateAsync(Reservation reservation);
    }
}