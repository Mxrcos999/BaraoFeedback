using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.DTOs.Ticket;
using BaraoFeedback.Infra.Querys;

namespace BaraoFeedback.Application.Services.Ticket;

public interface ITicketService
{
    Task<DefaultResponse> DeleteAsync(long entityId);
    Task<DefaultResponse> GetTicketAsync(TicketQuery query);
    Task<DefaultResponse> GetTicketByIdAsync(long id);
    Task<DefaultResponse> PostTicketAsync(TicketInsertRequest request);
}
