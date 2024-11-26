using Microsoft.AspNetCore.Http;

namespace Service.Services.Interfaces
{
    public interface IFileService
    {
        Task UploadImage(IFormFile image);
        Task DeleteImage(string name);
    }
}
