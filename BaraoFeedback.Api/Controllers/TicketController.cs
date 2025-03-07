using BaraoFeedback.Application.DTOs.Ticket;
using BaraoFeedback.Application.Services.Ticket;
using BaraoFeedback.Infra.Querys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaraoFeedback.Api.Controllers;

[ApiController]
[Route("/ticket")]
[Authorize]
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
        var result = await _tickerService.PostTicketAsync(request);
        return Ok(result);
    }

    [HttpGet]
    [Route("get-ticket")]
    public async Task<ActionResult<TicketResponse>> GetTicketAsync([FromQuery] TicketQuery request)
    {
        var result = await _tickerService.GetTicketAsync(request);

        return Ok(result);
    }

    [HttpGet]
    [Route("get-ticket-by-id")]
    public async Task<ActionResult<TicketResponse>> GetTicketByIdAsync(long ticketId)
    {
        var result = await _tickerService.GetTicketByIdAsync(ticketId);

        return Ok(result);
    }

    [HttpDelete("delete-ticket")]
    public async Task<IActionResult> DeleteInstitutionAsync(long ticketId)
    {
        var response = await _tickerService.DeleteAsync(ticketId);

        return Ok(response);
    }
}
