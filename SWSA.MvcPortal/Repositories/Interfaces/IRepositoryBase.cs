using SWSA.MvcPortal.Entities;
using SWSA.MvcPortal.Models;

namespace SWSA.MvcPortal.Repositories.Interfaces;

public interface IRepositoryBase<T> where T : class
{
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    void Update(T entity);
    void UpdateRange(IEnumerable<T> entities);
    void SaveChanges();
    Task SaveChangesAsync();
    Task<T?> GetByIdAsync(object id);
    Task<PagedResult<T>> GetPagedAsync(int pageIndex, int pageSize);
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetWithIncludedByIdAsync(object id);
}
