using SixLabors.ImageSharp.Formats;

namespace TravelAgency.Application.Helpers
{
    public class ImageHelpers
    {
        public static string GetContentType(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
            {
                return "application/octet-stream";
            }

            IImageFormat format = SixLabors.ImageSharp.Image.DetectFormat(imageData);
            return format?.DefaultMimeType ?? "application/octet-stream";
        }

        public static bool IsValidImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
            {
                return false;
            }
            IImageFormat format = Image.DetectFormat(imageData);
            return format != null;
        }
    }
}
