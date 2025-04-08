using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Models;
using SWSA.MvcPortal.Persistence;
using SWSA.MvcPortal.Repositories.Interfaces;

namespace SWSA.MvcPortal.Repositories.Repo;

public class RepositoryBase<T>(
    AppDbContext db
    ) : IRepositoryBase<T> where T : class
{
    #region CRUD

    public void Add(T entity)
    {
        db.Set<T>().Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        db.Set<T>().AddRange(entities);
    }

    public void Remove(T entity)
    {
        db.Set<T>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        db.Set<T>().RemoveRange(entities);
    }

    public void Update(T entity)
    {
        db.Set<T>().Update(entity);
    }

    public void UpdateRange(IEnumerable<T> entities)
    {
        db.Set<T>().UpdateRange(entities);
    }

    #endregion

    #region Save Changes

    public void SaveChanges()
    {
        db.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await db.SaveChangesAsync();
    }

    #endregion

    #region  Query

    public async Task<T?> GetByIdAsync(object id)
    {
        return await db.Set<T>().FindAsync(id);
    }

    #endregion

    #region Pagination

    public async Task<PagedResult<T>> GetPagedAsync(int pageIndex, int pageSize)
    {
        var query = db.Set<T>().AsQueryable();

        int totalCount = await query.CountAsync();
        var items = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<T>
        {
            Items = items,
            TotalCount = totalCount,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
    }

    #endregion

    #region Transaction

    public async Task BeginTransactionAsync()
    {
        await db.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await db.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await db.Database.RollbackTransactionAsync();
    }

    #endregion
}


