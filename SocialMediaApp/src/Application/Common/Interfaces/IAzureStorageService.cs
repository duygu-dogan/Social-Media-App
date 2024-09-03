using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace SocialMediaApp.Application.Common.Interfaces;
public interface IAzureStorageService
{
    Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files);
    Task DeleteAsync(string path, string fileName);
    List<string> GetFiles(string path);
    bool HasFile(string path, string fileName);
}
