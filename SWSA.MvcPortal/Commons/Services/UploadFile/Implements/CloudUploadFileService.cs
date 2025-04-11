using System.Net.Http;

namespace SWSA.MvcPortal.Commons.Services.UploadFile.Implements;

public class CloudUploadFileService(IConfiguration config,
    IHttpClientFactory clientFactory
    )
{
    private readonly HttpClient client = clientFactory.CreateClient("Cloud service client"); //Shoud setup at dependency injector

    public async Task<string> UploadAsync(IFormFile file, string subfolder)
    {
        //var connectionString = config["AzureBlob:ConnectionString"];
        //var containerName = config["AzureBlob:Container"];

        //var blobServiceClient = new BlobServiceClient(connectionString);
        //var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        //await containerClient.CreateIfNotExistsAsync();

        //var blobName = $"{subfolder}/{Guid.NewGuid():N}{Path.GetExtension(file.FileName)}";
        //var blobClient = containerClient.GetBlobClient(blobName);

        //using var stream = file.OpenReadStream();
        //await blobClient.UploadAsync(stream, overwrite: true);

        //return new UploadResult { FileName = file.FileName, FilePathOrUrl = blobClient.Uri.ToString() };

        return "Example";
    }

    public async Task<bool> DeleteAsync(string fileUrl)
    {
        //var uri = new Uri(fileUrl);
        //var blobName = uri.AbsolutePath.TrimStart('/');
        //var connectionString = _config["AzureBlob:ConnectionString"];
        //var containerName = _config["AzureBlob:Container"];

        //var blobServiceClient = new BlobServiceClient(connectionString);
        //var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        //var blobClient = containerClient.GetBlobClient(blobName);

        //var result = await blobClient.DeleteIfExistsAsync();
        //return result.Value;
        return true;
    }
}
