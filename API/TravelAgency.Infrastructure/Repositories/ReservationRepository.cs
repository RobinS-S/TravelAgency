using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Repositories.Interfaces;
using TravelAgency.Infrastructure.Data;

namespace TravelAgency.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly TravelAgencyDbContext dbContext;

        public ReservationRepository(TravelAgencyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(Reservation reservation)
        {
            dbContext.Reservations.Add(reservation);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var reservation = await dbContext.Reservations.FindAsync(id);
            if (reservation != null)
            {
                dbContext.Reservations.Remove(reservation);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await dbContext.Reservations
                .Include(r => r.Flights)
                .Include(r => r.Residence)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetAllByResidenceIdAndBetweenAsync(long residenceId, DateTime start, DateTime end)
        {
            return await dbContext.Reservations
                .Include(r => r.Flights)
                .Include(r => r.Residence)
                .Where(r => r.Id == residenceId && r.Start >= start && r.End <= end).ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetAllByResidenceIdAsync(long residenceId)
        {
            return await dbContext.Reservations
                .Include(r => r.Flights)
                .Include(r => r.Residence)
                .Where(r => r.ResidenceId == residenceId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetAllByTenantIdAsync(string userId)
        {
            return await dbContext.Reservations
                .Include(r => r.Flights)
                .Include(r => r.Residence)
                .Where(r => r.Tenant.Id == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetAllByOwnerIdAsync(string ownerId)
        {
            return await dbContext.Reservations
                .Include(r => r.Flights)
                .Include(r => r.Residence)
                .Where(r => r.Owner.Id == ownerId)
                .ToListAsync();
        }

        public async Task<Reservation?> GetByIdAsync(long id)
        {
            return await dbContext.Reservations
                .Include(r => r.Flights)
                .Include(r => r.Residence)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            dbContext.Reservations.Update(reservation);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Reservation?> GetActiveByTenantIdAsync(string tenantId)
        {
            return await dbContext.Reservations
                .Include(r => r.Flights)
                .Include(r => r.Residence)
                .FirstOrDefaultAsync(r => r.TenantId == tenantId && r.End >= DateTime.Now);
        }

        public async Task<IEnumerable<Reservation>> GetActiveByOwnerIdAsync(string ownerId)
        {
            return await dbContext.Reservations
                .Include(r => r.Flights)
                .Include(r => r.Residence)
                .Where(r => r.OwnerId == ownerId && r.End >= DateTime.Now).ToListAsync();
        }
    }
}
