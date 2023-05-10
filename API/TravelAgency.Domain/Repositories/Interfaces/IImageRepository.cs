using TravelAgency.Domain.Entities;

namespace TravelAgency.Domain.Repositories.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> AddAsync(Image image);
        Task DeleteAsync(long id);
        Task<IEnumerable<Image>> GetAllAsync();
        Task<Image?> GetByIdAsync(long id);
        Task UpdateAsync(Image image);
    }
}