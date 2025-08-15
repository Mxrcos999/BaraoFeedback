using BaraoFeedback.Application.DTOs.Ticket;
using BaraoFeedback.Application.Extensions;
using BaraoFeedback.Application.Interfaces;
using BaraoFeedback.Infra.Context;
using BaraoFeedback.Application.DTOs.Querys;
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

    public async Task<IQueryable<TicketResponse>> GetTicketAsync(TicketQuery query)
    {
        var tickets = (from data in _context.Ticket
                      .AsNoTracking()
                      .Where(query.CreateFilterExpression())
                       select new TicketResponse()
                       {
                           CategoryName = data.TicketCategory.Description,
                           Description = data.Description,
                           Processed = data.Processed,
                           CreatedAt = data.CreatedAt.ToString("dd/MM/yyyy"),
                           InstitutionName = data.Institution.Name,
                           LocationName = data.Location.Name,
                           Title = data.Title,
                           StudentCode = data.ApplicationUser.UserName,
                           StudentName = data.ApplicationUser.Name,
                           TicketId = data.Id,
                       });

        return tickets;
    } 

    public async Task<TicketResponse> GetTicketByIdAsync(long entityId)
    {
        var ticket = (from data in _context.Ticket
                      .AsNoTracking()
                      .Where(x => x.Id == entityId)
                       select new TicketResponse()
                       {
                           CategoryName = data.TicketCategory.Description,
                           Description = data.Description,
                           CreatedAt = data.CreatedAt.ToString("dd/MM/yyyy"),
                           InstitutionName = data.Institution.Name,
                           LocationName = data.Location.Name,
                           Processed = data.Processed,
                           Title = data.Title,
                           StudendMail = data.ApplicationUser.Email,
                           StudentCode = data.ApplicationUser.UserName,
                           StudentName = data.ApplicationUser.Name,
                           TicketId = data.Id,
                       }).FirstOrDefault();

        return ticket;
    } 

    public async Task<bool> PostTicketAsync(Domain.Entities.Ticket entity)
    {
        await _context.Ticket.AddAsync(entity);

        var result = await _context.SaveChangesAsync(default);

        if (result > 0)
            return true;

        return false;
     }

    public async Task<IQueryable<TicketReportResponse>> GetTicketReportAsync(TicketQuery query)
    {
        var tickets = (from data in _context.Ticket
                      .AsNoTracking()
                      .Where(query.CreateFilterExpression())
                       select new TicketReportResponse()
                       {
                           TicketId = data.Id,
                           Title = data.Title,
                           Description = data.Description,
                           StudentCode = data.ApplicationUser.UserName,
                           StudentName = data.ApplicationUser.Name,
                           StudentEmail = data.ApplicationUser.Email,
                           InstitutionName = data.Institution.Name,
                           LocationName = data.Location.Name,
                           CategoryName = data.TicketCategory.Description,
                           Status = data.Processed ? "Processado" : "Pendente",
                           CreatedAt = data.CreatedAt.ToString("dd/MM/yyyy HH:mm"),
                       });

        return tickets;
    }
}
