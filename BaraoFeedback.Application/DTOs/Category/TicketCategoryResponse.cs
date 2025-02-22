namespace BaraoFeedback.Application.DTOs.Category;

public class TicketCategoryResponse
{
    public long DescriptionId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Description { get; set; }
    public int TicketQuantity { get; set; }
}
