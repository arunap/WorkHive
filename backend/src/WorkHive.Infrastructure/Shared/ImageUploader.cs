using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using WorkHive.Application.Abstraction;
using WorkHive.Application.Abstraction.Context;
using WorkHive.Domain.Exceptions;

namespace WorkHive.Infrastructure.Shared
{
    public class ImageUploader : IImageUploader
    {
        private readonly IApplicationDbContext _context;
                private readonly IDateTimeProvider _dateTimeProvider;
        private readonly string _fileUploadPath;
        private readonly string _imageUploadPath;

        public ImageUploader(IApplicationDbContext context, IConfiguration configuration, IHostEnvironment hostEnvironment, IDateTimeProvider dateTimeProvider)
        {
            _context = context;
            _dateTimeProvider = dateTimeProvider;

            _imageUploadPath = configuration["ImageUploadPath"] ?? "Uploads/Images";
            _fileUploadPath = Path.Combine(hostEnvironment.ContentRootPath, "wwwroot", _imageUploadPath);
            Directory.CreateDirectory(_fileUploadPath);
        }

        public async Task<Guid> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0) throw new FileUploadException("Invalid file");

            if (file.Length > 2 * 1024 * 1024) throw new FileUploadException("File size should be less than 2MB");

            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(_fileUploadPath, fileName);
            string serverImgPath = Path.Combine(_imageUploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            Guid newId = Guid.NewGuid();
            _context.FileStores.Add(new Domain.FileInfo.FileStore
            {
                Id = newId,
                ContentType = file.ContentType,
                FileName = file.FileName,
                FilePath = serverImgPath,
                FileSize = file.Length / 1024,
            });

            await _context.SaveChangesAsync();

            return newId;
        }
    }
}