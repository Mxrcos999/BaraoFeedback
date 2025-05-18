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
    public DateTime? InitialDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool? IsInactive { get; set; }
    public long? LocationId { get; set; }

    public Expression<Func<TicketCategory, bool>> CreateFilterExpression()
    {
        var predicate = PredicateBuilder.True<TicketCategory>();
        if(IsInactive is null)
            predicate = predicate.And(x => x.IsActive == true);

        if(IsInactive.Value)
            predicate = predicate.And(x => x.IsActive == false);
        else
            predicate = predicate.And(x => x.IsActive == true);

        if (CategoryId is not null && CategoryId > 0)
            predicate = predicate.And(x => x.Id == CategoryId);

        if (InstitutionId is not null && InstitutionId > 0)
            predicate = predicate.And(x => x.Tickets.Any(x => x.InstitutionId == InstitutionId));
        
        if (LocationId is not null && LocationId > 0)
            predicate = predicate.And(x => x.Tickets.Any(x => x.LocationId == LocationId));

        if (InitialDate is not null)
            predicate = predicate.And(x => x.Tickets.Any(t => t.CreatedAt >= InitialDate.Value));

        if (EndDate is not null)
            predicate = predicate.And(x => x.Tickets.Any(t => t.CreatedAt <= EndDate.Value));
        return predicate;
    }
}
