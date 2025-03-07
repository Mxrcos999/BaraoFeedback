using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.DTOs.Ticket;
using BaraoFeedback.Application.Interfaces;
using BaraoFeedback.Application.Services.Email;
using BaraoFeedback.Infra.Querys; 
namespace BaraoFeedback.Application.Services.Ticket;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IEmailService _emailService;

    public TicketService(ITicketRepository ticketRepository, IEmailService emailService)
    {
        _ticketRepository = ticketRepository;
        _emailService = emailService;
    }

    public async Task<DefaultResponse> GetTicketAsync(TicketQuery query)
    {
        var response = new DefaultResponse();

        response.Data = await _ticketRepository.GetTicketAsync(query);

        return response;
    }
    public async Task<DefaultResponse> GetTicketByIdAsync(long id)
    {
        var response = new DefaultResponse();

        response.Data = await _ticketRepository.GetTicketByIdAsync(id);

        return response;
    }
    public async Task<DefaultResponse> PostTicketAsync(TicketInsertRequest request)
    {
        var response = new DefaultResponse();
        var entity = new Domain.Entities.Ticket()
        {
            ApplicationUserId = _ticketRepository.GetUserId(),
            Description = request.Description,
            InstitutionId = request.InstitutionId,
            TicketCategoryId = request.CategoryId,
            LocationId = request.LocationId,
            Title = request.Title, 
        };
        response.Data = await _ticketRepository.PostTicketAsync(entity);

   //     _emailService.SendEmail("Marcos", "marcosfelipehd4@gmail.com");

        return response;
    }

    public async Task<DefaultResponse> DeleteAsync(long entityId)
    {
        var response = new DefaultResponse();
        var entity = await _ticketRepository.GetByIdAsync(entityId);
        response.Data = await _ticketRepository.DeleteAsync(entity, default);

        return response;
    }
}
