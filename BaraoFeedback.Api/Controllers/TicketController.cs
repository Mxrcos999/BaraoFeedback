using BaraoFeedback.Application.DTOs.Ticket;
using BaraoFeedback.Application.Services.Ticket;
using BaraoFeedback.Application.DTOs.Querys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaraoFeedback.Api.Controllers;

[ApiController]
[Route("/ticket")] 
public class TicketController : ControllerBase
{
    private readonly ITicketService _tickerService;
    public TicketController(ITicketService tickerService)
    {
        _tickerService = tickerService;
    }

    [HttpPost]
    [Route("post-ticket")]
    public async Task<IActionResult> PostTicketAsync(TicketInsertRequest request)
    {
        var response = await _tickerService.PostTicketAsync(request);

        if (!response.Sucess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpGet]
    [Route("get-ticket")]
    public async Task<ActionResult<TicketResponse>> GetTicketAsync([FromQuery] TicketQuery request)
    {
        var response = await _tickerService.GetTicketAsync(request);

        if (!response.Sucess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpGet]
    [Route("get-ticket-by-id")]
    public async Task<ActionResult<TicketResponse>> GetTicketByIdAsync(long ticketId)
    {
        var response = await _tickerService.GetTicketByIdAsync(ticketId);

        if (!response.Sucess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPatch]
    [Route("process-ticket")]
    public async Task<ActionResult<TicketResponse>> ProcessTicketAsync([FromQuery] long ticketId,[FromBody] UpdateTicket ticket)
    {
        var response = await _tickerService.ProcessTicketAsync(ticketId, ticket.Status);

        if (!response.Sucess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpDelete("delete-ticket")]
    public async Task<IActionResult> DeleteInstitutionAsync(long ticketId)
    {
        var response = await _tickerService.DeleteAsync(ticketId);

        if (!response.Sucess)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpGet]
    [Route("generate-report")]
    public async Task<IActionResult> GenerateTicketReportAsync([FromQuery] TicketQuery query)
    {
        try
        {
            var excelBytes = await _tickerService.GenerateTicketReportAsync(query);
            
            var fileName = $"relatorio_tickets_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            
            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Erro ao gerar relatório", error = ex.Message });
        }
    }
}

