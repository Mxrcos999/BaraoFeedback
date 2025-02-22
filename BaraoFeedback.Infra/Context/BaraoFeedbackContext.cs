using BaraoFeedback.Domain.Entities;
using BaraoFeedback.Domain.Entities.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BaraoFeedback.Infra.Context;

public class BaraoFeedbackContext : IdentityDbContext<ApplicationUser>
{
    public readonly IHttpContextAccessor _httpContextAccessor;

    public BaraoFeedbackContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        foreach (var entry in ChangeTracker.Entries<Entity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
            } 
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //builder.ApplyConfiguration(new ApplicationUserMap()); 
    }
    public DbSet<TicketCategory> TicketCategory { get; set; } 
    public DbSet<Ticket> Ticket { get; set; } 
    public DbSet<Institution> Institution { get; set; } 
}
