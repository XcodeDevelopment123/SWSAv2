using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Services.Interfaces.WorkAssignments;

public interface IWorkAssignmentUpdater<T> where T : Entities.WorkAssignment
{
    int TaskId { get; }
    int CompanyId { get; }
    void ApplyTo(T entity);
}
