using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.Services.Email;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Text;

namespace BaraoFeedback.Application.Services.Email;

public class EmailService : IEmailService
{
    private readonly EmailSenderOptions _options;

    public EmailService(IOptions<EmailSenderOptions> options)
    {
        _options = options.Value;
    }
    private SmtpClient ObterClient()
    {
        var client = new SmtpClient("smtp.titan.email", 587)
        {
            Credentials = new System.Net.NetworkCredential("suporte@paintballtropadechoque.com", "Quita123*"),
            EnableSsl = true
        };
        return client;

    }

    private SmtpClient ObterClientTeste()
    {
        var client = new SmtpClient("smtp.titan.email", 587)
        {
            Credentials = new System.Net.NetworkCredential("", ""),
            EnableSsl = true
        };
        return client;

    }

    public DefaultResponse SendEmail(string nome, string destinatarios)
    {
        if (string.IsNullOrEmpty(destinatarios) ||
        string.IsNullOrEmpty(nome))
        {
            throw new ArgumentException("Os parâmetros remetente, destinatário, assunto e corpoo do email são obrigatórios.");
        }
        var listaDestinatarios = destinatarios.Split(",");
        var response = new DefaultResponse();

        try
        {
            using (var mm = new MailMessage("suporte@.com", listaDestinatarios[0]))
            {
                foreach (var emailAtual in listaDestinatarios)
                {
                    mm.To.Add(new MailAddress(emailAtual.TrimStart().TrimEnd()));
                }
                mm.Subject = $"Seu agendamento esta quase concluído!";
                mm.IsBodyHtml = true;
                mm.Body = @$"
                    <html>
                    <head>
                        <style>
                            .content {{
                                justify-content: center;
                                align-items: center;
                                text-align: center;
                            }}
                            .payment-button {{
                                color: white;
                                background-color: black;
                                padding: 10px 20px;
                                text-decoration: none;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='content'>
                            <h3>Olá, {nome}<br>Seu agendamento está quase concluído.</h3>
                            <img src='https://paintballstorage.blob.core.windows.net/imagespaintball/logo-paintball.jpg' alt='logo' width='300' height='200'>

                            <p>Por favor, efetue seu pagamento para finalizar o agendamento clicando no botão abaixo:</p>
                            <br>
                            <br>
                            <a  class='payment-button'>Efetuar pagamento</a>
                            <br>
                            <br>
                        </div>
                    </body>
                    </html>";

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
