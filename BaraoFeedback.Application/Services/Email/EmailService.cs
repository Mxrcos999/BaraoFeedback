using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.DTOs.Ticket;
using BaraoFeedback.Application.Services.Email;
using BaraoFeedback.Application.Services.User;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace BaraoFeedback.Application.Services.Email;

public class EmailService : IEmailService
{
    private readonly EmailSenderOptions _options;
    private readonly IIdentityService _service;

    public EmailService(IOptions<EmailSenderOptions> options, IIdentityService service)
    {
        _options = options.Value;
        _service = service;
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
            <p><strong>Título:</strong> {ticket.Title}</p>
            <p><strong>Descrição:</strong><br />{ticket.Description}</p>
            <hr />
            <p><strong>Aluno:</strong> {ticket.StudentName} ({ticket.StudentCode})</p>
            <p><strong>Instituição:</strong> {ticket.InstitutionName}</p>
            <p><strong>Local:</strong> {ticket.LocationName}</p>
            <p><strong>Categoria:</strong> {ticket.CategoryName}</p>
            <p><strong>Data de Abertura:</strong> {ticket.CreatedAt}</p>
        </body>
        </html>";
    }


    public async Task<DefaultResponse> SendEmail(TicketResponse ticket)
    { 
        var listaDestinatarios = await _service.GetEmailsAdmin();
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
