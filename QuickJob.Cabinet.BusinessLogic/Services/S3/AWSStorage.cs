using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using QuickJob.Cabinet.DataModel.Configuration;
using QuickJob.Cabinet.DataModel.Exceptions;
using Vostok.Configuration.Abstractions;
using Vostok.Logging.Abstractions;

namespace QuickJob.Cabinet.BusinessLogic.Services.S3;

public sealed class AWSStorage : IS3Storage
{
    private readonly ILog log;
    private readonly AmazonS3Client s3Client;
    private readonly S3Settings s3Settings;

    public AWSStorage(ILog log, AmazonS3Client s3Client, IConfigurationProvider configurationProvider)
    {
        this.log = log.ForContext(nameof(AWSStorage));
        this.s3Client = s3Client;
        s3Settings = configurationProvider.Get<S3Settings>();
    }

    public async Task<string> UploadFile(IFormFile file)
    {
        try
        {
            var fileUrl= await UploadFileInternal(file);
            
            log.Info($"Successfully uploaded file: '{file.FileName}'");
            return fileUrl;
        }
        catch (Exception e)
        {
            log.Error($"Error uploaded file; StackTrace: '{e.StackTrace}'.");
            throw new CustomHttpException(HttpStatusCode.ServiceUnavailable, HttpErrors.AWS(e.Message));
        }
    }

    public async Task DeleteFile(string filePath)
    {
        var key = filePath.Replace(s3Settings.BucketBaseUrl, string.Empty);
        var result = await s3Client.DeleteObjectAsync(s3Settings.BucketName, key);
        if (result.HttpStatusCode == HttpStatusCode.InternalServerError)
        {
            log.Error($"Error delete file; ResponseMetadata: '{result.ResponseMetadata}'.");
            //todo: ?как не ломать сценарий пользователя? throw new CustomHttpException(HttpStatusCode.ServiceUnavailable, HttpErrors.AWS(result.ResponseMetadata));
        }
        log.Info($"Successfully deleted file: '{key}'");
    }

    private async Task<string> UploadFileInternal(IFormFile file)
    {
        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var request = new PutObjectRequest
        {
            BucketName = s3Settings.BucketName,
            Key = $"{s3Settings.RootPath}/{fileName}",
            InputStream = file.OpenReadStream(),
            ContentType = file.ContentType
        };
        
        await s3Client.PutObjectAsync(request);
        
        return $"{s3Settings.FilesBaseUrl}/{fileName}";
    }
}