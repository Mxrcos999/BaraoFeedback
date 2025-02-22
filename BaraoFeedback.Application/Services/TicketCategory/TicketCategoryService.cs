using BaraoFeedback.Application.DTOs.Category;
using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.Interfaces;
using BaraoFeedback.Infra.Querys;

namespace BaraoFeedback.Application.Services.TicketCategory;

public class TicketCategoryService : ITicketCategoryService
{
    private readonly ITicketCategoryRepository _ticketCategoryRepository;

    public TicketCategoryService(ITicketCategoryRepository ticketCategoryRepository)
    {
        _ticketCategoryRepository = ticketCategoryRepository;
    }

    public async Task<DefaultResponse> InsertTicketCategoryAsync(TicketCategoryInsertRequest request)
    {
        var response = new DefaultResponse();

        response.Data = await _ticketCategoryRepository.PostCategoryTicketAsync(new Domain.Entities.TicketCategory()
        {
            Description = request.Description,

        });

        return response;
    }

    public async Task<DefaultResponse> GetTicketCategoryAsync(TicketCategoryQuery query)
    {
        var response = new DefaultResponse();

        var data = await _ticketCategoryRepository.GetTicketCategoryAsync(query);

        if (query.IsDescending is null)
            response.Data = data.OrderBy(x => x.CreatedAt);
     
        if(query.IsDescending is not null)
        {
            if (query.IsDescending.Value)
                response.Data = data.OrderByDescending(x => x.TicketQuantity);

            if (!query.IsDescending.Value)
            {
                data.Sort();
                response.Data = data;
            }
        }

        return response;
    }
    public async Task<DefaultResponse> GetCategoryAsync()
    {
        var response = new DefaultResponse();
         
         response.Data = await _ticketCategoryRepository.GetCategoryAsync();
        return response;
    }
}
