using Microsoft.AspNetCore.Http;

namespace QuickJob.Cabinet.BusinessLogic.Services.S3;

public interface IS3Storage
{
    Task<string> UploadFile(IFormFile file);
    Task DeleteFile(string filePath);
}