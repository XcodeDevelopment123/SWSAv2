
namespace SWSA.MvcPortal.Commons.Quartz.Services.Interfaces;

[Obsolete("currently use job instead of service")]
public interface IAssignmentDueSoonJobService
{
    Task ProcessDueSoonAssignmentsAsync();
}

