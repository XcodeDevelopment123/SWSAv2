using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Entities.Templates;
using SWSA.MvcPortal.Persistence;

namespace SWSA.MvcPortal.Controllers.SecretaryDept;

[Route("secretary-dept")]
public class SecretaryController(
       AppDbContext db
    ) : BaseController
{
    private readonly DbSet<SecDeptTaskTemplate> _tasks = db.Set<SecDeptTaskTemplate>();

    #region Page/View
    [Route("landing")]
    public async Task<IActionResult> Landing()
    {
        var data = await _tasks.Include(c => c.Client).ToListAsync();
        return View(data);
    }
    #endregion

    #region API/Ajax

    #endregion
}
