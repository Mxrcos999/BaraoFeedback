using BaraoFeedback.Domain.Entities.Base;

namespace BaraoFeedback.Domain.Entities;

public sealed class Ticket : Entity
{
    public string Title { get; set; }
    public string Description { get; set; } 
    public bool Processed { get; set; } 
    public ApplicationUser ApplicationUser { get; set; }
    public string ApplicationUserId { get; set; }
    public TicketCategory TicketCategory { get; set; }
    public long TicketCategoryId { get; set; } 
    public Institution Institution { get; set; }
    public long InstitutionId { get; set; } 
    public Location Location { get; set; }
    public long LocationId { get; set; }
}
