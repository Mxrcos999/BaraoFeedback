using BaraoFeedback.Application.DTOs.Ticket;
using BaraoFeedback.Domain.Entities;
using BaraoFeedback.Infra.Querys;

namespace BaraoFeedback.Application.Interfaces;

public interface ITicketRepository : IGenericRepository<Ticket>
{
    Task<bool> PostTicketAsync(Domain.Entities.Ticket entity);
    Task<List<TicketResponse>> GetTicketAsync(TicketQuery query);
}
