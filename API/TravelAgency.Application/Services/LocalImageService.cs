using Microsoft.AspNetCore.Http;
using System.IO;
using TravelAgency.Application.Helpers;
using TravelAgency.Application.Services.Interfaces;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Repositories.Interfaces;
using Image = TravelAgency.Domain.Entities.Image;

namespace TravelAgency.Application.Services
{
    public class LocalImageService : IImageService
    {
        private readonly IImageRepository imageRepository;
        private readonly Config config;

        public LocalImageService(IImageRepository imageRepository, Config config)
        {
            this.imageRepository = imageRepository;
            this.config = config;
        }

        public async Task<Image?> AddImage(IFormFile imageFile, ApplicationUser user)
        {
            var fileName = $"{Path.GetRandomFileName()}.{Path.GetExtension(imageFile.FileName)}";

            using (MemoryStream stream = new())
            {
                await imageFile.CopyToAsync(stream);
                var imageData = stream.ToArray();
                if(!ImageHelpers.IsValidImage(imageData))
                {
                    return null;
                }

                var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
                if(!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                using FileStream fileStream = new(Path.Combine(GetImageStorageFolder(), $"Images\\{fileName}"), FileMode.Create);
                await imageFile.CopyToAsync(fileStream);
            }

            var imageUrl = $"{config.AppUrl}/images/{fileName}";
            var image = await imageRepository.AddAsync(new Image(imageUrl, user));
            return image;
        }

        public async Task<Image?> GetImage(long id)
        {
            var image = await imageRepository.GetByIdAsync(id);
            return image;
        }

        public async Task<byte[]?> GetImageData(long id)
        {
            var image = await imageRepository.GetByIdAsync(id);
            if (image == null)
            {
                return null;
            }

            var filePath = Path.Combine(GetImageStorageFolder(), $"Images\\" + image.ImageUrl[image.ImageUrl.LastIndexOf('/')..]); 
            if (!File.Exists(filePath))
            {
                return null;
            }

            return await File.ReadAllBytesAsync(filePath);
        }

        public async Task<bool> RemoveImage(long id)
        {
            var image = await imageRepository.GetByIdAsync(id);
            if (image == null)
            {
                return false;
            }

            var filePath = Path.Combine(GetImageStorageFolder(), $"Images\\" + image.ImageUrl[image.ImageUrl.LastIndexOf('/')..]);
            if (!File.Exists(filePath))
            {
                await Task.Factory.StartNew(() => File.Delete(filePath));
            }

            await imageRepository.DeleteAsync(id);
            return true;
        }

        private static string GetImageStorageFolder() => AppDomain.CurrentDomain.BaseDirectory;
    }
}
