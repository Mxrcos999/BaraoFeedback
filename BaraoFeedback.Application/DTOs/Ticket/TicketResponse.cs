namespace BaraoFeedback.Application.DTOs.Ticket;

public class TicketResponse
{
    public long TicketId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string StudentCode { get; set; }
    public string StudentName { get; set; }
    public string StudendMail { get; set; }
    public string InstitutionName { get; set; }
    public string LocationName { get; set; }
    public string CategoryName { get; set; }
    public string CreatedAt { get; set; }
}
