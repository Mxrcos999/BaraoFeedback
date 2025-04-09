using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.DTOs.Ticket;

namespace BaraoFeedback.Application.Services.Email;

public interface IEmailService
{
    Task<DefaultResponse> SendEmail(TicketResponse ticket);
}
