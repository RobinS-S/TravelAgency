using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Entities;
using TravelAgency.Infrastructure.Data;

namespace TravelAgency.Infrastructure.Repositories
{
    public class ResidenceRepository
    {
        private readonly TravelAgencyDbContext _context;

        public ResidenceRepository(TravelAgencyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Residence>> GetAllAsync() =>
            await _context.Residences.ToListAsync();

        public async Task<Residence?> GetByIdAsync(long id) =>
            await _context.Residences.FindAsync(id);

        public async Task AddAsync(Residence residence)
        {
            await _context.Residences.AddAsync(residence);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Residence residence)
        {
            _context.Residences.Update(residence);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var residence = await _context.Residences.FindAsync(id);
            if (residence != null)
            {
                _context.Residences.Remove(residence);
                await _context.SaveChangesAsync();
            }
        }
    }

}
