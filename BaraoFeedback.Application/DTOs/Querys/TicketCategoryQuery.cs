using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Domain.Entities;
using LinqKit;
using System.Linq.Expressions;

namespace BaraoFeedback.Infra.Querys;

public class TicketCategoryQuery : BaseGetRequest
{
    public bool? IsDescending { get; set; } = null;
    public long? CategoryId { get; set; }
    public long? InstitutionId { get; set; }
    public long? LocationId { get; set; }

    public Expression<Func<TicketCategory, bool>> CreateFilterExpression()
    {
        var predicate = PredicateBuilder.True<TicketCategory>();

        if (CategoryId is not null && CategoryId > 0)
            predicate = predicate.And(x => x.Id == CategoryId);

        if (InstitutionId is not null && InstitutionId > 0)
            predicate = predicate.And(x => x.Tickets.Any(x => x.InstitutionId == InstitutionId));
        
        if (LocationId is not null && LocationId > 0)
            predicate = predicate.And(x => x.Tickets.Any(x => x.LocationId == LocationId));

        return predicate;
    }
}
