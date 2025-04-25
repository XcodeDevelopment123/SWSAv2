using SWSA.MvcPortal.Commons.Services.UploadFile.Implements;

namespace SWSA.MvcPortal.Commons.Services.UploadFile;

public class UploadFileService(
    LocalUploadFileService localService,
    CloudUploadFileService cloudService
    ) : IUploadFileService
{
    public async Task<string> UploadAsync(IFormFile file, string subfolder, UploadStorageType storageType = UploadStorageType.Local)
    {
        return storageType switch
        {
            UploadStorageType.Local => await localService.UploadAsync(file, subfolder),
            UploadStorageType.Cloud => await cloudService.UploadAsync(file, subfolder),
            _ => throw new NotImplementedException()
        };
    }

    public async Task<bool> DeleteAsync(string pathOrUrl, UploadStorageType storageType)
    {
        return storageType switch
        {
            UploadStorageType.Local => localService.Delete(pathOrUrl),
            UploadStorageType.Cloud => await cloudService.DeleteAsync(pathOrUrl),
            _ => false
        };
    }

    public string SanitizeFolderName(string input)
    {
        foreach (var c in Path.GetInvalidFileNameChars())
            input = input.Replace(c.ToString(), "");

        return input.Trim().Replace(" ", "_");
    }
}
