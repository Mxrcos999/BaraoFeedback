using BaraoFeedback.Application.DTOs.Ticket;
using BaraoFeedback.Application.Services.Ticket;
using BaraoFeedback.Infra.Querys;
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
    public async Task<ActionResult<TicketResponse>> ProcessTicketAsync(UpdateTicket ticket)
    {
        var response = await _tickerService.ProcessTicketAsync(ticket.TicketId);

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
}

