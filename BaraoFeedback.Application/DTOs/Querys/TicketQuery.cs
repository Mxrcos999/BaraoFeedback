using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Domain.Entities;
using LinqKit;
using System.Linq.Expressions;

namespace BaraoFeedback.Application.DTOs.Querys;

public class TicketQuery : BaseGetRequest
{
    public long? InstitutionId { get; set; }
    public long? LocationId { get; set; }
    public long? CategoryId { get; set; }
    public bool? Process { get; set; }
    public string? StudentCode { get; set; }
    public DateTime? InitialDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Expression<Func<Domain.Entities.Ticket, bool>> CreateFilterExpression()
    {
        var predicate = PredicateBuilder.True<Domain.Entities.Ticket>();

        if(Process is not null)
            predicate = predicate.And(x => x.Processed == Process);
       

        if (InstitutionId is not null && InstitutionId > 0)
            predicate = predicate.And(x => x.InstitutionId == InstitutionId);

        if (LocationId is not null && LocationId > 0)
            predicate = predicate.And(x => x.LocationId == LocationId);

        if (CategoryId is not null && CategoryId > 0)
            predicate = predicate.And(x => x.TicketCategoryId == CategoryId);

        if (!string.IsNullOrEmpty(StudentCode))
            predicate = predicate.And(x => x.ApplicationUser.Type == "student" && x.ApplicationUser.UserName == StudentCode);

        if (InitialDate is not null)
            predicate = predicate.And(x => x.CreatedAt >= InitialDate);

        if (EndDate is not null)
            predicate = predicate.And(x => x.CreatedAt <= EndDate);

        return predicate;
    }
}
