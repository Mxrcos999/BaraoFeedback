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
    public async Task<DefaultResponse> ProcessTicketAsync(long id)
    {
        var response = new DefaultResponse();

        var ticket = new Domain.Entities.Ticket()
        {
            Id = id,
            Processed = true
        };

        response.Data = await _ticketRepository.UpdateAsync(ticket, default);

        return response;
    }
    public async Task<DefaultResponse> PostTicketAsync(TicketInsertRequest request)
    {
        var response = new DefaultResponse();
        var entity = new Domain.Entities.Ticket()
        {
            ApplicationUserId = "2032a7cf-3da5-4940-8f54-2f0174120c2d",//_ticketRepository.GetUserId(),
            Description = request.Description,
            InstitutionId = request.InstitutionId,
            TicketCategoryId = request.CategoryId,
            LocationId = request.LocationId,
            Title = request.Title, 
        };
        var result = await _ticketRepository.PostTicketAsync(entity);
        response.Data = result;

        var ticket = await _ticketRepository.GetTicketByIdAsync(entity.Id);
        await _emailService.SendEmail(ticket);

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
