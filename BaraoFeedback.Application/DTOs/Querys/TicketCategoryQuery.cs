using BaraoFeedback.Domain.Entities;
using LinqKit;
using System.Linq.Expressions;

namespace BaraoFeedback.Infra.Querys;

public class TicketCategoryQuery
{
    public bool? IsDescending { get; set; } = null;
    public long CategoryId { get; set; }

    public Expression<Func<TicketCategory, bool>> CreateFilterExpression()
    {
        var predicate = PredicateBuilder.True<TicketCategory>();

        return predicate;
    }
}
