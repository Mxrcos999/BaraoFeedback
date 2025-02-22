using System.Linq.Expressions;
using System.Linq;
using System.Security.Claims;
using BaraoFeedback.Infra.Context;
using Microsoft.EntityFrameworkCore;
using BaraoFeedback.Application.Interfaces;

namespace BaraoFeedback.Infra.Repositories.Shared;


public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    public readonly BaraoFeedbackContext _context;
    public readonly DbSet<TEntity> _entity;
    public readonly string _currentUserId;

    protected GenericRepository(BaraoFeedbackContext context)
    {
        _context = context;
        _currentUserId = _context._httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        _entity = _context.Set<TEntity>();

    }
    public async Task<int> GetTotalPages(Expression<Func<TEntity, bool>>? filterExpression = null, double? quantityItems = null)
    {
        return (int)Math.Ceiling(_entity.Where(filterExpression != null ? filterExpression : x => true).Count() / (double)quantityItems);
    }

    public virtual async Task<IEnumerable<object>> GetAsync(Expression<Func<TEntity, bool>>? filterExpression = null)
    {
        var dbSet = TrackingItem(false);
        if (filterExpression is null)
            return dbSet.AsEnumerable();

        return dbSet.Where(filterExpression).AsEnumerable();
    }

    public virtual async Task<TEntity> GetByIdAsync(long id)
    {
        return await _entity.FindAsync(id);
    }

    public virtual async Task<bool> PostAsync(TEntity entity, CancellationToken ct)
    {
        try
        {
            await _entity.AddAsync(entity);

            var result = await _context.SaveChangesAsync(ct);

            return result > 0 ? true : false;
        }
        catch (Exception)
        {

            throw;
        }

    }

    public virtual async Task<bool> PostRangeAsync(List<TEntity> entityList, CancellationToken ct)
    {
        try
        {
            await _entity.AddRangeAsync(entityList);

            var result = await _context.SaveChangesAsync(ct);

            return result > 0 ? true : false;
        }
        catch (Exception)
        {

            throw;
        }

    }

    public virtual async Task<bool> UpdateAsync(TEntity entity, CancellationToken ct)
    {
        _entity.Update(entity);

        var result = await _context.SaveChangesAsync(ct);

        return result > 0 ? true : false;
    }

    public virtual async Task<bool> DeleteAsync(TEntity entity, CancellationToken ct)
    {
        _entity.Remove(entity);

        var result = await _context.SaveChangesAsync(ct);

        return result > 0 ? true : false;
    }

    public virtual async Task<bool> DeleteRangeAsync(List<TEntity> entity, CancellationToken ct)
    {
        _entity.RemoveRange(entity);

        var result = await _context.SaveChangesAsync(ct);

        return result > 0 ? true : false;
    }

    private IQueryable<TEntity> TrackingItem(bool tracking)
    {
        if (tracking)
            return _entity;

        return _entity.AsNoTracking();
    }
}
