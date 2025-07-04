using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Services.Interfaces.WorkAssignment;

public interface IWorkAssignmentUpdater<T> where T : CompanyWorkAssignment
{
    int TaskId { get; }
    int CompanyId { get; }
    void ApplyTo(T entity);
}
