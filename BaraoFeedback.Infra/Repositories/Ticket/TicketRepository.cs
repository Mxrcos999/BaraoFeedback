using BaraoFeedback.Application.DTOs.Ticket;
using BaraoFeedback.Application.Interfaces;
using BaraoFeedback.Infra.Context;
using BaraoFeedback.Infra.Querys;
using BaraoFeedback.Infra.Repositories.Shared;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BaraoFeedback.Infra.Repositories.Ticket;

public class TicketRepository : GenericRepository<Domain.Entities.Ticket>, ITicketRepository
{
    private readonly BaraoFeedbackContext _context;
    public readonly string UserId;

    public TicketRepository(BaraoFeedbackContext context) : base(context)
    {
        _context = context;
        UserId = _context._httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public async Task<List<TicketResponse>> GetTicketAsync(TicketQuery query)
    {
        var tickets = (from data in _context.Ticket
                      .AsNoTracking()
                      .Where(query.CreateFilterExpression())
                       select new TicketResponse()
                       {
                           CategoryName = data.TicketCategory.Description,
                           Description = data.Description,
                           CreatedAt = data.CreatedAt.ToString("dd/MM/yyyy"),
                           InstitutionName = data.Institution.Name,
                           Title = data.Title,
                           StudentCode = data.ApplicationUser.UserName,
                           StudentName = data.ApplicationUser.Name,
                           TicketId = data.Id,
                       }).ToList();

        return tickets;
    } 

    public async Task<bool> PostTicketAsync(Domain.Entities.Ticket entity)
    {
        await _context.Ticket.AddAsync(entity);

        var result = await _context.SaveChangesAsync(default);

        if (result > 0)
            return true;

        return false;
    } 
}
