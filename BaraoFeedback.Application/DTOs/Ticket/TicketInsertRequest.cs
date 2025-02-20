namespace BaraoFeedback.Application.DTOs.Ticket;

public class TicketInsertRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public long InstitutionId { get; set; }
    public long CategoryId { get; set; }
}
