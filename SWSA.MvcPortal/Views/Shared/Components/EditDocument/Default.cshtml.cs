using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Views.Shared.Components.EditDocument;

public class EditDocumentViewComponent() : ViewComponent
{
    public IViewComponentResult Invoke(List<DocumentRecord> input)
    {
        return View(input);
    }
}
