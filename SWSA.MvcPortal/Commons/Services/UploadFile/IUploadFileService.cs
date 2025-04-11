namespace SWSA.MvcPortal.Commons.Services.UploadFile;

public interface IUploadFileService
{
    Task<string> UploadAsync(IFormFile file, string subfolder, UploadStorageType storageType = UploadStorageType.Local);
    Task<bool> DeleteAsync(string pathOrUrl, UploadStorageType storageType = UploadStorageType.Local);
}