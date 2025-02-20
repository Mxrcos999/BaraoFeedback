using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.DTOs.Ticket;
using BaraoFeedback.Infra.Querys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaraoFeedback.Api.Controllers;

[ApiController]
[Route("/ticket")]
public class TicketController : ControllerBase
{
    public TicketController() { }

    [HttpPost]
    [Route("ticket/post-ticket")]
    public async Task<IActionResult> PostTicketAsync(TicketInsertRequest request)
    {
        return Ok(new DefaultResponse());
    }

    [HttpGet]
    [Route("ticket/get-ticket")]
    public async Task<ActionResult<TicketResponse>> GetTicketAsync([FromQuery] TicketQuery request)
    {
        return Ok(new DefaultResponse() 
        { 
            Data = new List<TicketResponse>()
            {
                new TicketResponse()
                {
                    CategoryName = "Teste categoria",
                    CreatedAt = "20/02/2025",
                    StudentName = "Aluno",
                    TicketId = 999,
                    Description = "Descrição teste",
                    InstitutionName = "Barão",
                    StudentCode = "1249845",
                    Title = "Tituloo teste"
                }, new TicketResponse()
                {
                    CategoryName = "Teste categoria",
                    CreatedAt = "20/02/2025",
                    StudentName = "Aluno",
                    TicketId = 999,
                    Description = "Descrição teste",
                    InstitutionName = "Barão",
                    StudentCode = "1249845",
                    Title = "Tituloo teste"
                }, new TicketResponse()
                {
                    CategoryName = "Teste categoria",
                    CreatedAt = "20/02/2025",
                    StudentName = "Aluno",
                    TicketId = 999,
                    Description = "Descrição teste",
                    InstitutionName = "Barão",
                    StudentCode = "1249845",
                    Title = "Tituloo teste"
                }, new TicketResponse()
                {
                    CategoryName = "Teste categoria",
                    CreatedAt = "20/02/2025",
                    StudentName = "Aluno",
                    TicketId = 999,
                    Description = "Descrição teste",
                    InstitutionName = "Barão",
                    StudentCode = "1249845",
                    Title = "Tituloo teste"
                }, new TicketResponse()
                {
                    CategoryName = "Teste categoria",
                    CreatedAt = "20/02/2025",
                    StudentName = "Aluno",
                    TicketId = 999,
                    Description = "Descrição teste",
                    InstitutionName = "Barão",
                    StudentCode = "1249845",
                    Title = "Tituloo teste"
                },
            }
        });
    }

    [HttpGet]
    [Route("ticket/get-ticket-by-id")]
    public async Task<ActionResult<TicketResponse>> GetTicketByIdAsync(long ticketId)
    {
        return Ok(new TicketResponse());
    }
}
