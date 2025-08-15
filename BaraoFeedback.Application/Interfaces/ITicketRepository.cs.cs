using BaraoFeedback.Application.DTOs.Ticket;
using BaraoFeedback.Domain.Entities;
using BaraoFeedback.Application.DTOs.Querys;

namespace BaraoFeedback.Application.Interfaces;

public interface ITicketRepository : IGenericRepository<Ticket>
{
    Task<bool> PostTicketAsync(Domain.Entities.Ticket entity);
    Task<TicketResponse> GetTicketByIdAsync(long entityId);
    Task<IQueryable<TicketResponse>> GetTicketAsync(TicketQuery query);
    Task<IQueryable<TicketReportResponse>> GetTicketReportAsync(TicketQuery query);
}
