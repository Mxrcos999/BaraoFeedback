using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.DTOs.Ticket;

namespace BaraoFeedback.Application.Services.Email;

public interface IEmailService
{
    Task<BaseResponse<bool>> SendConfirmMail(string mail, string name, string link);
    Task<BaseResponse<bool>> SendEmail(TicketResponse ticket);
    Task<BaseResponse<bool>> SendForgotPasswordEmail(string email, string userName, string senha);
    Task<BaseResponse<bool>> SendPassword(string mail, string name, string password);
}
