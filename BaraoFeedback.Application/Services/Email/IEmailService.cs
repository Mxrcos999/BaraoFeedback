using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.DTOs.Ticket;

namespace BaraoFeedback.Application.Services.Email;

public interface IEmailService
{
    Task<DefaultResponse> SendConfirmMail(string mail, string name, string link);
    Task<DefaultResponse> SendEmail(TicketResponse ticket);
    Task<DefaultResponse> SendForgotPasswordEmail(string email, string userName, string senha);
    Task<DefaultResponse> SendPassword(string mail, string name, string password);
}
