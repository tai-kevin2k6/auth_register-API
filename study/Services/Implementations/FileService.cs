using Microsoft.Extensions.Options;
using study.Model.DTOs;
using study.Services.Interfaces;
using System.Security.Cryptography;

namespace study.Services.Implementations;

public class FileService : IFileService
{
    public async Task<UploadResult> SaveImageAsync(IFormFile file, HttpContext http)
    {
        //check empty file
        if (file == null || file.Length == 0) 
        {
            throw new ArgumentException("Empty file"); 
        }

        ////check size file
        //long max = _opt.MaxFileSizeMB * 1024L * 1024L;
        //if (file.Length > max) 
        //{
        //    throw new InvalidOperationException($"Max file size: {_opt.MaxFileSizeMB}");
        //}

        ////check type file
        //var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        //if (!_opt.AllowedExtension.Contains(ext))
        //{
        //    throw new InvalidOperationException($"{ext}");
        //}


        //gerenate path, dir
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var saveDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        Directory.CreateDirectory(saveDir); 
        var savePath = Path.Combine(saveDir, fileName);

        //save file to server
        await using var stream = new FileStream(savePath, FileMode.Create); // KHÔNG phải CreateNew
        await file.CopyToAsync(stream);

        //gerenate URL public for file
        var req = http.Request;
        var baseUrl = $"{req.Scheme}://{req.Host}";
        var url = $"{baseUrl}/uploads/{fileName}";

        return new UploadResult(fileName, url, file.Length);
    }
} 
    