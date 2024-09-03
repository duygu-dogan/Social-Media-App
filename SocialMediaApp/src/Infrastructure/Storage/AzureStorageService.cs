using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SocialMediaApp.Application.Common.Interfaces;

namespace SocialMediaApp.Infrastructure.Storage;
internal class AzureStorageService : FileRenameExtension, IAzureStorageService
{
    readonly BlobServiceClient _blobServiceClient;
    BlobContainerClient? _blobContainerClient;

    public AzureStorageService(IConfiguration configuration)
    {
        _blobServiceClient = new (configuration["Storage:Azure"]);
    }
    public async Task DeleteAsync(string path, string fileName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(path);
        BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
        await blobClient.DeleteIfExistsAsync();
    }

    public List<string> GetFiles(string path)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(path);
        return _blobContainerClient.GetBlobs().Select(b => b.Name).ToList();
    }

    public new bool HasFile(string path, string fileName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(path);
        return _blobContainerClient.GetBlobs().Any(b => b.Name == fileName);
    }

    public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(path);
        await _blobContainerClient.CreateIfNotExistsAsync();
        await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

        List<(string fileName, string path)> datas = new();
        foreach (var file in files)
        {
            string newFileName = await FileRenameAsync(path, file.FileName, HasFile);
            BlobClient blobClient = _blobContainerClient.GetBlobClient(newFileName);
            await blobClient.UploadAsync(file.OpenReadStream());
            datas.Add((newFileName, $"https://socialmediaapp.blob.core.windows.net/{path}/{newFileName}"));
        }
        return datas;
    }
}
