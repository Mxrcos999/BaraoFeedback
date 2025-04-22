using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.DTOs.Ticket;
using BaraoFeedback.Application.Services.Email;
using BaraoFeedback.Application.Services.User;
using BaraoFeedback.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace BaraoFeedback.Application.Services.Email;

public class EmailService : IEmailService
{
    private readonly EmailSenderOptions _options;
    private readonly UserManager<ApplicationUser> _userManager;

    public EmailService(IOptions<EmailSenderOptions> options, UserManager<ApplicationUser> userManager)
    {
        _options = options.Value;
        _userManager = userManager;
    }
    private SmtpClient ObterClient()
    {
        var client = new SmtpClient("smtp.office365.com", 587)
        {
            Credentials = new System.Net.NetworkCredential("fabricadesoftware@baraodemaua.edu.br", "=Eup@5b+"),
            EnableSsl = true 
        };
        return client;
    }
    private string GerarCorpoEmail(TicketResponse ticket)
    {
        return $@"
        <html>
        <body style='font-family: Arial, sans-serif;'>
            <h2 style='color: #2e6c80;'>Novo Chamado Recebido</h2>
            <p><strong>ID do Chamado:</strong> {ticket.TicketId}</p> 
            <p><strong>Descrição:</strong><br />{ticket.Description}</p>
            <hr />
            <p><strong>Aluno:</strong> {ticket.Title}</p>
            <p><strong>Instituição:</strong> {ticket.InstitutionName}</p>
            <p><strong>Local:</strong> {ticket.LocationName}</p>
            <p><strong>Categoria:</strong> {ticket.CategoryName}</p>
            <p><strong>Data de Abertura:</strong> {ticket.CreatedAt}</p>
        </body>
        </html>";
    }
    public static string GenerateConfirmationEmailBody(string userName, string confirmationLink)
    {
        return $@"
    <html>
        <body style='font-family: Arial, sans-serif; background-color: #f9f9f9; padding: 20px;'>
            <div style='max-width: 600px; margin: auto; background-color: #ffffff; padding: 30px; border-radius: 10px; box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);'>
                <h2 style='color: #333333;'>Olá, {userName}!</h2>
                <p style='color: #555555; font-size: 16px;'>
                    Obrigado por se registrar. Para ativar sua conta, clique no botão abaixo para confirmar seu e-mail:
                </p>
                <a href='{confirmationLink}' 
                   style='display: inline-block; padding: 12px 24px; background-color: #007bff; color: white; text-decoration: none; border-radius: 6px; font-weight: bold; margin-top: 20px;'>
                    Confirmar E-mail
                </a>
                <p style='color: #555555; font-size: 14px; margin-top: 30px;'>
                    Ou copie e cole o seguinte link no seu navegador:
                </p>
                <p style='word-break: break-all; color: #007bff; font-size: 14px;'>
                    <a href='{confirmationLink}' style='color: #007bff;'>{confirmationLink}</a>
                </p>
                <p style='margin-top: 40px; font-size: 12px; color: #999999;'>
                    Se você não criou esta conta, pode ignorar este e-mail.
                </p>
            </div>
        </body>
    </html>";
    }

    public async Task<DefaultResponse> SendConfirmMail(string mail, string name, string link)
    {
        var response = new DefaultResponse();

        try
        {
            using (var mm = new MailMessage("fabricadesoftware@baraodemaua.edu.br", mail))
            {
                mm.To.Add(new MailAddress(mail));

                mm.Subject = $"Confirme seu email";
                mm.IsBodyHtml = true;
                mm.Body = GenerateConfirmationEmailBody(name, link);

                mm.BodyEncoding = Encoding.GetEncoding("UTF-8");
                using (var client = ObterClient())
                {
                    client.Send(mm);
                }

                return response;
            }
        }
        catch (SmtpFailedRecipientsException ex)
        {
            List<string> destinatariosInvalidos = new List<string>();
            foreach (var failedRecipient in ex.InnerExceptions)
            {
                if (failedRecipient is SmtpFailedRecipientException failedRecipientEx)
                {
                    destinatariosInvalidos.Add(failedRecipientEx.FailedRecipient);
                }
            }
            response.Errors.AddError($"Email a seguir é inválido: {string.Join(", ", destinatariosInvalidos.Distinct())}");

            return response;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<DefaultResponse> SendEmail(TicketResponse ticket)
    {
        var listaDestinatarios = _userManager.Users.Where(x => x.Type == "admin").Select(x => x.Email).ToArray();
        var response = new DefaultResponse();

        try
        {
            using (var mm = new MailMessage("fabricadesoftware@baraodemaua.edu.br", listaDestinatarios[0]))
            {
                foreach (var emailAtual in listaDestinatarios)
                {
                    string padrao = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                    if(Regex.IsMatch(emailAtual, padrao))
                    {
                        mm.To.Add(new MailAddress(emailAtual.TrimStart().TrimEnd()));
                    }
                }
                mm.Subject = $"Novo chamado #{ticket.TicketId}";
                mm.IsBodyHtml = true;
                mm.Body = GerarCorpoEmail(ticket);

                mm.BodyEncoding = Encoding.GetEncoding("UTF-8");
                using (var client = ObterClient())
                {
                    client.Send(mm);
                } 

                return response;
            }
        }
        catch (SmtpFailedRecipientsException ex)
        {
            List<string> destinatariosInvalidos = new List<string>();
            foreach (var failedRecipient in ex.InnerExceptions)
            {
                if (failedRecipient is SmtpFailedRecipientException failedRecipientEx)
                {
                    destinatariosInvalidos.Add(failedRecipientEx.FailedRecipient);
                }
            }
            response.Errors.AddError($"Email a seguir é inválido: {string.Join(", ", destinatariosInvalidos.Distinct())}");
             
            return response;

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


}


public class EmailSenderOptions
{
    public string FromName { get; set; }
    public string FromEmail { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
