using Microsoft.AspNetCore.Mvc;

namespace SWSA.MvcPortal.Controllers;

[Route("files")]
public class FileController : BaseController
{
    [HttpGet("download")]
    public async Task<IActionResult> Download([FromQuery] string path, [FromQuery] string? fileOriName)
    {
        //TO DO: Secure download, use token / one time download
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Datas", "uploads", path.TrimStart('/'));
        if (!System.IO.File.Exists(fullPath))
            return NotFound();

        var contentType = "application/octet-stream";
        var fileName = Path.GetFileName(fullPath);
        var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);

        return File(stream, contentType, fileOriName ?? fileName);
    }

    [HttpGet("view")]
    public IActionResult View([FromQuery] string path)
    {
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Datas", "uploads", path.TrimStart('/'));

        if (!System.IO.File.Exists(fullPath))
            return NotFound();

        var extension = Path.GetExtension(fullPath).ToLowerInvariant();
        var contentType = extension switch
        {
            ".pdf" => "application/pdf",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".json" => "application/json",
            ".csv" => "text/csv",
            ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ".xls" => "application/vnd.ms-excel",
            ".txt" => "text/plain",
            _ => "application/octet-stream"
        };

        //Html view

        /**
         <iframe src="/file/view?path=docs/company-a/reports/data.json"
        width="100%" height="500px" frameborder="0">
            </iframe>
         */
        var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
        return File(stream, contentType);
    }
}
