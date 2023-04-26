using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Repositories.Interfaces;
using TravelAgency.Infrastructure.Data;

namespace TravelAgency.Infrastructure.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly TravelAgencyDbContext _context;

        public ImageRepository(TravelAgencyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Image>> GetAllAsync() =>
            await _context.Images.ToListAsync();

        public async Task<Image?> GetByIdAsync(long id) =>
            await _context.Images.FindAsync(id);

        public async Task<Image> AddAsync(Image image)
        {
            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();
            return image;
        }

        public async Task UpdateAsync(Image image)
        {
            _context.Images.Update(image);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image != null)
            {
                _context.Images.Remove(image);
                await _context.SaveChangesAsync();
            }
        }
    }

}
