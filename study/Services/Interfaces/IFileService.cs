using study.Model.DTOs;

namespace study.Services.Interfaces;

public interface IFileService
{
    Task<UploadResult> SaveImageAsync(IFormFile file, HttpContext http);
}
