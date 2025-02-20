using BaraoFeedback.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BaraoFeedback.Infra.Context;

public class BaraoFeedbackContext : IdentityDbContext<ApplicationUser>
{
    public BaraoFeedbackContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<TicketCategory> TicketCategory { get; set; } 
    public DbSet<Ticket> Ticket { get; set; } 
    public DbSet<Institution> Institution { get; set; } 
}
