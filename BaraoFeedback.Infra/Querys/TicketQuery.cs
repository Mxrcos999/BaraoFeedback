using BaraoFeedback.Domain.Entities;

namespace BaraoFeedback.Infra.Querys;

public class TicketQuery
{
    public long? InstitutionId { get; set; }
    public long? CategoryId { get; set; }
    public string? StudentCode { get; set; }
    public DateTime? InitialDate { get; set; }
    public DateTime? EndDate { get; set; }
    public IQueryable<Ticket> CreateFilterExpression(IQueryable<Ticket> query)
    {
        return query.Where(x => x.Description == "");
    }
}
