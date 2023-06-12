using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using TravelAgency.Application.Helpers;
using TravelAgency.Application.Services.Interfaces;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Repositories.Interfaces;
using Image = TravelAgency.Domain.Entities.Image;

namespace TravelAgency.Application.Services
{
    public class AzureBlobImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly Config _config;
        private readonly BlobServiceClient _blobServiceClient;
        private const string DefaultContainerName = "images";

        public AzureBlobImageService(IImageRepository imageRepository, Config config, BlobServiceClient blobServiceClient)
        {
            this._imageRepository = imageRepository;
            this._config = config;
            this._blobServiceClient = blobServiceClient;
        }

        private string GetContainerName()
        {
            return _config.AzureStorageContainerName ?? DefaultContainerName;
        }

        public async Task<Image?> AddImage(IFormFile imageFile, ApplicationUser user)
        {
            var fileName = $"{Path.GetRandomFileName()}{Path.GetExtension(imageFile.FileName)}";

            using (MemoryStream stream = new())
            {
                await imageFile.CopyToAsync(stream);
                var imageData = stream.ToArray();
                if (!ImageHelpers.IsValidImage(imageData))
                {
                    return null;
                }
                
                var containerClient = _blobServiceClient.GetBlobContainerClient(GetContainerName());
                var blobClient = containerClient.GetBlobClient(fileName);

                stream.Position = 0;
                await blobClient.UploadAsync(stream, overwrite: true);
            }
            
            var image = await _imageRepository.AddAsync(new Image(fileName, user));
            return image;
        }
        
        public async Task<Image?> GetImage(long id)
        {
            var image = await _imageRepository.GetByIdAsync(id);
            return image;
        }

        public async Task<byte[]?> GetImageData(long id)
        {
            var image = await _imageRepository.GetByIdAsync(id);
            if (image == null)
            {
                return null;
            }

            var blobClient = _blobServiceClient.GetBlobContainerClient(GetContainerName()).GetBlobClient(image.ImageUrl);
            if (!await blobClient.ExistsAsync())
            {
                return null;
            }

            var download = await blobClient.DownloadAsync();
            using MemoryStream ms = new();
            if (download == null)
            {
                return null;
            }
            await download.Value.Content.CopyToAsync(ms);
            return ms.ToArray();
        }

        public async Task<bool> RemoveImage(long id)
        {
            var image = await _imageRepository.GetByIdAsync(id);
            if (image == null)
            {
                return false;
            }

            var blobClient = _blobServiceClient.GetBlobContainerClient(GetContainerName()).GetBlobClient(image.ImageUrl);
            if (await blobClient.ExistsAsync())
            {
                await blobClient.DeleteAsync();
            }

            await _imageRepository.DeleteAsync(id);
            return true;
        }
    }
}
