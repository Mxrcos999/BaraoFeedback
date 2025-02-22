using BaraoFeedback.Domain.Entities;
using LinqKit;
using System.Linq.Expressions;

namespace BaraoFeedback.Infra.Querys;

public class TicketQuery
{
    public long? InstitutionId { get; set; }
    public long? CategoryId { get; set; }
    public string? StudentCode { get; set; }
    public DateTime? InitialDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Expression<Func<Ticket, bool>> CreateFilterExpression()
    {
        var predicate = PredicateBuilder.True<Ticket>();

        return predicate;
    }
}
