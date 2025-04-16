using Microsoft.AspNetCore.Mvc;

namespace SWSA.MvcPortal.Controllers;

[Route("errors")]
public class ErrorController : BaseController
{
    [Route("NotFound")]
    public IActionResult NotFoundPage()
    {
        Response.StatusCode = 404;
        return View("NotFound");
    }


    [Route("BusinessError")]
    public IActionResult BusinessError(string? message = null)
    {
        Response.StatusCode = 400;

        ViewData["ErrorMessage"] = string.IsNullOrEmpty(message)
            ? "Operation not permitted by current business rules."
            : message;

        return View("BusinessError");
    }

    [Route("ServerError")]
    public IActionResult ServerError()
    {
        Response.StatusCode = 500;
        return View("ServerError");
    }

    [Route("Forbidden")]
    public IActionResult Forbidden()
    {
        Response.StatusCode = 403;
        return View("Forbidden");
    }
}
