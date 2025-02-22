using BaraoFeedback.Domain.Entities.Base;

namespace BaraoFeedback.Domain.Entities;

public class TicketCategory : Entity
{
    public string Description { get; set; }
    public List<Ticket>? Tickets { get; set; }
}
