using Microsoft.AspNetCore.Hosting;
using CleanArchitecture.Domain.Interfaces.Server;

namespace CleanArchitecture.Infrastructure.Server
{
    public class ImageStorageLocalServerAdapter : IImageStorageService
    {
        private IWebHostEnvironment _hostingEnvironment;

        public ImageStorageLocalServerAdapter(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<string> UploadImageAsync(byte[] image, string fileName)
        {
            var crestRelativeUrl = Path.Combine("uploads/images", fileName);
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, crestRelativeUrl);

            // if not exists, create directory
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            await File.WriteAllBytesAsync(filePath, image);
            return crestRelativeUrl;
        }

        public Task DeleteImageAsync(string imageRelativeURl)
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, imageRelativeURl);
            File.Delete(filePath);
            return Task.CompletedTask;
        }
    }
}
