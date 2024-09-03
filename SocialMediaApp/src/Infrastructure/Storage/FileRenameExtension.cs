using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMediaApp.Application.Common.Helpers;

namespace SocialMediaApp.Infrastructure.Storage;
internal class FileRenameExtension
{
    protected delegate bool HasFile(string path, string fileName);
    protected async Task<string> FileRenameAsync(string path, string fileName, HasFile hasFileMethod)
    {
        string newFileName = await Task.Run(() =>
        {
            string extension = Path.GetExtension(fileName);
            string oldName = Path.GetFileNameWithoutExtension(fileName);
            string newFileName = $"{NameRegulatoryHelper.CharRegulatory(oldName)}{extension}";
            if (hasFileMethod(path, newFileName))
            {
                int i = 1;
                while (hasFileMethod(path, $"{oldName}-{i}{extension}"))
                    i++;
                return newFileName = $"{oldName}-{i}{extension}";
            }
            else
                return newFileName;
        });
        return newFileName;
    }
}
