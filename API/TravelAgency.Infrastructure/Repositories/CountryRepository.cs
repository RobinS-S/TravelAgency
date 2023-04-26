using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Repositories.Interfaces;
using TravelAgency.Infrastructure.Data;

namespace TravelAgency.Infrastructure.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly TravelAgencyDbContext _context;

        public CountryRepository(TravelAgencyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Country>> GetAllAsync() =>
            await _context.Countries.ToListAsync();

        public async Task<Country?> GetByIdAsync(long id) =>
            await _context.Countries.FindAsync(id);

        public async Task AddAsync(Country country)
        {
            await _context.Countries.AddAsync(country);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Country country)
        {
            _context.Countries.Update(country);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country != null)
            {
                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();
            }
        }
    }

}
