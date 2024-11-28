using Microsoft.AspNetCore.Http;

namespace WorkHive.Application.Abstraction
{
    public interface  IImageUploader
    {
         Task<Guid> UploadFileAsync(IFormFile file);
    }
}