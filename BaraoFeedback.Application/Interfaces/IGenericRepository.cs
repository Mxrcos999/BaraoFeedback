using System.Linq.Expressions;

namespace BaraoFeedback.Application.Interfaces;


public interface IGenericRepository<TEntity> where TEntity : class
{
    string GetUserId();
    Task<IEnumerable<object>> GetAsync(Expression<Func<TEntity, bool>>? filterExpression = null);
    Task<TEntity> GetByIdAsync(long id);
    Task<bool> PostAsync(TEntity entity, CancellationToken ct);
    Task<bool> PostRangeAsync(List<TEntity> entityList, CancellationToken ct);
    Task<bool> UpdateAsync(TEntity entity, CancellationToken ct);
    Task<bool> DeleteAsync(TEntity entity, CancellationToken ct);
    Task<bool> DeleteRangeAsync(List<TEntity> entity, CancellationToken ct);
    Task<int> GetTotalPages(Expression<Func<TEntity, bool>>? filterExpression = null, double? quantityItems = null);
}