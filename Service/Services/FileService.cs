using Microsoft.AspNetCore.Http;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class FileService : IFileService
    {

        public async Task DeleteImage(string name)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "uploads", name);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        public async Task UploadImage(IFormFile image)
        {
            string fileExtension = Path.GetExtension(image.FileName);
            string fileName = Guid.NewGuid().ToString() + fileExtension;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fileName);

            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "uploads")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "uploads"));
            }

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
        }

    }
}
