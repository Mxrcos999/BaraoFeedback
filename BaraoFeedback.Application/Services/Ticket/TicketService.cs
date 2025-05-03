using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.DTOs.Ticket;
using BaraoFeedback.Application.Extensions;
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

    public async Task<BaseResponse<List<TicketResponse>>> GetTicketAsync(TicketQuery query)
    {
        var response = new BaseResponse<List<TicketResponse>>();
        var data = (await _ticketRepository.GetTicketAsync(query)).Pagination<TicketResponse>(query.BaseGetRequest);
        var totalRecord = (await _ticketRepository.GetTicketAsync(query)).Count();
        
        response.TotalRecords = totalRecord;
        response.PageSize = data.Count();
        response.Page = query.BaseGetRequest.Page;  
        response.Data = data.ToList();
        return response;
    }
    public async Task<BaseResponse<TicketResponse>> GetTicketByIdAsync(long id)
    {
        var response = new BaseResponse<TicketResponse>();

        response.Data = await _ticketRepository.GetTicketByIdAsync(id);

        return response;
    }
    public async Task<BaseResponse<bool>> ProcessTicketAsync(long id)
    {
        var response = new BaseResponse<bool>();

        var ticket = await _ticketRepository.GetByIdAsync(id);

        response.Data = await _ticketRepository.UpdateAsync(ticket, default);

        return response;
    }
    public async Task<BaseResponse<bool>> PostTicketAsync(TicketInsertRequest request)
    {
        var response = new BaseResponse<bool>();
        var entity = new Domain.Entities.Ticket()
        {
            ApplicationUserId = _ticketRepository.GetUserId(),
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

    public async Task<BaseResponse<bool>> DeleteAsync(long entityId)
    {
        var response = new BaseResponse<bool>();
        var entity = await _ticketRepository.GetByIdAsync(entityId);
        response.Data = await _ticketRepository.DeleteAsync(entity, default);

        return response;
    }
}
