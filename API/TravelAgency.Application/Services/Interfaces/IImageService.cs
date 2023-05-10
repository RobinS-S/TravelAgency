using Microsoft.AspNetCore.Http;
using TravelAgency.Domain.Entities;

namespace TravelAgency.Application.Services.Interfaces
{
    public interface IImageService
    {
        Task<Domain.Entities.Image?> AddImage(IFormFile imageFile, ApplicationUser user);
        Task<Domain.Entities.Image?> GetImage(long id);
        Task<byte[]?> GetImageData(long id);
        Task<bool> RemoveImage(long id);
    }
}