using Microsoft.EntityFrameworkCore;
using SWSA.MvcPortal.Models;
using SWSA.MvcPortal.Persistence;
using SWSA.MvcPortal.Repositories.Interfaces;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace SWSA.MvcPortal.Repositories.Repo;

public class RepositoryBase<T>(
    AppDbContext db
    ) : IRepositoryBase<T> where T : class
{
    private static readonly ConcurrentDictionary<Type, string> _keyNameCache = new();
    private readonly DbSet<T> _dbSet = db.Set<T>();


    #region CRUD
    public virtual void Add(T entity)
    {
        BeforeAdd(entity);
        _dbSet.Add(entity);
    }

    public virtual void AddRange(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            BeforeAdd(entity);
        }
        _dbSet.AddRange(entities);
    }

    public virtual void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public virtual async Task RemoveAsync(T entity)
    {
        await BeforeRemove(entity);
        _dbSet.Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            BeforeRemove(entity);
        }
        _dbSet.RemoveRange(entities);
    }

    public virtual async Task RemoveRangeAsync(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            await BeforeRemove(entity);
        }
        _dbSet.RemoveRange(entities);
    }


    public virtual void Update(T entity)
    {
        BeforeUpdate(entity);
        _dbSet.Update(entity);
    }

    public virtual void UpdateRange(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            BeforeUpdate(entity);
        }
        _dbSet.UpdateRange(entities);
    }

    //Provide for repository rewrite the method
    protected virtual Task BeforeAdd(T entity) => Task.CompletedTask;

    protected virtual Task BeforeRemove(T entity) => Task.CompletedTask;

    protected virtual Task BeforeUpdate(T entity) => Task.CompletedTask;

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
    //For the repository to rewrite the method
    protected virtual Task<IQueryable<T>> BuildGetByIdQueryAsync()
    {
        return Task.FromResult(db.Set<T>().AsQueryable());
    }

    protected virtual Task<IQueryable<T>> BuildQueryAsync()
    {
        return Task.FromResult(db.Set<T>().AsQueryable());
    }

    protected virtual Task<IQueryable<T>> BuildQueryWithIncludesAsync()
    {
        return Task.FromResult(db.Set<T>().AsQueryable());
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        var query = await BuildQueryAsync();
        return await query.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(object id)
    {
        var query = await BuildGetByIdQueryAsync();
        return await query.FirstOrDefaultAsync(BuildKeyLambda(id));
    }

    public virtual async Task<T?> GetWithIncludedByIdAsync(object id)
    {
        var query = await BuildQueryWithIncludesAsync();

        return await query.FirstOrDefaultAsync(BuildKeyLambda(id));
    }
    #endregion

    #region Pagination

    public async Task<PagedResult<T>> GetPagedAsync(int pageIndex, int pageSize)
    {
        var query = await BuildQueryAsync();
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

    #region Helper
    private string GetPrimaryKeyName(Type entityType)
    {
        return _keyNameCache.GetOrAdd(entityType, type =>
        {
            var efEntity = db.Model.FindEntityType(type);
            var key = efEntity?.FindPrimaryKey()?.Properties.FirstOrDefault()?.Name;
            return key ?? throw new InvalidOperationException($"No primary key defined for entity {type.Name}");
        });
    }

    private Expression<Func<T, bool>> BuildKeyLambda(object id)
    {
        var keyName = GetPrimaryKeyName(typeof(T));
        var parameter = Expression.Parameter(typeof(T), "e");
        var property = Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(object) }, parameter, Expression.Constant(keyName));
        var equals = Expression.Equal(property, Expression.Convert(Expression.Constant(id), typeof(object)));
        return Expression.Lambda<Func<T, bool>>(equals, parameter);
    }

    #endregion

}


