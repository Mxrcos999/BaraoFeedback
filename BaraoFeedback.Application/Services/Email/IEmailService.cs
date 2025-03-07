using BaraoFeedback.Application.DTOs.Shared;

namespace BaraoFeedback.Application.Services.Email;

public interface IEmailService
{
    DefaultResponse SendEmail(string nome, string destinatarios);
}
