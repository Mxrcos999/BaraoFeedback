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

        if(InstitutionId is not null && InstitutionId > 0)
            predicate = predicate.And(x => x.InstitutionId == InstitutionId);
        
        if(CategoryId is not null && CategoryId > 0)
            predicate = predicate.And(x => x.TicketCategoryId == CategoryId);
        
        if(!string.IsNullOrEmpty(StudentCode))
            predicate = predicate.And(x => x.ApplicationUser.Type == "student" && x.ApplicationUser.UserName == StudentCode);

        if (InitialDate is not null)
            predicate = predicate.And(x => x.CreatedAt >= InitialDate); 

        if (EndDate is not null)
            predicate = predicate.And(x => x.CreatedAt <= EndDate);

        return predicate;
    }
}
