using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WorkHive.Application.Abstraction
{
    public interface  IImageUploader
    {
         Task<Guid> UploadFileAsync(IFormFile file);
    }
}