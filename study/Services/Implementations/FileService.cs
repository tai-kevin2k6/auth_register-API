using Microsoft.Extensions.Options;
using study.Model.DTOs;
using study.Services.Interfaces;
using System.Security.Cryptography;

namespace study.Services.Implementations;

public class FileService(IOptions<UploadOptions> opt) : IFileService
{
    private readonly UploadOptions _opt = opt.Value;

    public async Task<UploadResult> SaveImageAsync(IFormFile file, HttpContext http)
    {
        //check empty file
        if (file == null || file.Length == 0) 
        {
            throw new ArgumentNullException("Empty file"); 
        }
        //check size file
        long max = _opt.MaxFileSizeMB * 1024L * 1024L;
        if (file.Length > max) 
        {
            throw new InvalidOperationException($"Max file size: {_opt.MaxFileSizeMB}");
        }
        //check type file
        var ext = Path.GetExtension(file.Name).ToLowerInvariant();
        if (!_opt.AllowedExtension.Contains(ext))
        {
            throw new InvalidOperationException($"{ext}");
        }
        //gerenate safe name for file
        var safeName = GetSafeRandomName(ext);
        //gerenate path, dir
        var saveDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        Directory.CreateDirectory(saveDir); 
        var savePath = Path.Combine(saveDir, safeName);

        //save file to server
        using (var stream = new FileStream(savePath, FileMode.CreateNew))
        {
            await file.CopyToAsync(stream);
        }
        //gerenate URL public for file
        var req = http.Request;
        var baseUrl = $"{req.Scheme}://{req.Host}";
        var url = $"{baseUrl}/uploads/{safeName}";

        return new UploadResult(safeName, url, file.Length);
    }

    private static string GetSafeRandomName(string ext)
    {
        var bytes = RandomNumberGenerator.GetBytes(16);
        var name = Convert.ToHexString(bytes).ToLowerInvariant();
        return $"{name}{ext}";
    }
} 
    