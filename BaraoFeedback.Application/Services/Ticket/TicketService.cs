using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.DTOs.Ticket;
using BaraoFeedback.Application.Interfaces;
using BaraoFeedback.Infra.Querys;
using BaraoFeedback.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BaraoFeedback.Application.Services.Ticket;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;

    public TicketService(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<DefaultResponse> GetTicketAsync(TicketQuery query)
    {
        var response = new DefaultResponse();

        response.Data = await _ticketRepository.GetTicketAsync(query);

        response.Sucess = true;
        return response;
    }
    public async Task<DefaultResponse> PostTicketAsync(TicketInsertRequest request)
    {
        var response = new DefaultResponse();
        var entity = new Domain.Entities.Ticket()
        {
        };
        response.Data = await _ticketRepository.PostTicketAsync(entity);

        response.Sucess = true;

        return response;
    }
}
