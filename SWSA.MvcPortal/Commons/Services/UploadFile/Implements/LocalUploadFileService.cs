namespace SWSA.MvcPortal.Commons.Services.UploadFile.Implements;

public class LocalUploadFileService(
    IWebHostEnvironment env
    )
{
    public async Task<string> UploadAsync(IFormFile file, string subfolder)
    {
        var rootPath = Directory.GetCurrentDirectory();
        var savePath = Path.Combine(rootPath, "Datas", "uploads", subfolder);
        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var fullPath = Path.Combine(savePath, fileName);

        using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);

        var relativePath = Path.Combine(subfolder, fileName).Replace("\\", "/");
        return relativePath;
    }

    public bool Delete(string relativePath)
    {
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath.Replace("/", Path.DirectorySeparatorChar.ToString()));
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
            return true;
        }
        return false;
    }
}
