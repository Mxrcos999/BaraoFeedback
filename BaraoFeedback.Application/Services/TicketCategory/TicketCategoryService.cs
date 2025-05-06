using BaraoFeedback.Application.DTOs.Category;
using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.Extensions;
using BaraoFeedback.Application.Interfaces;
using BaraoFeedback.Infra.Querys;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BaraoFeedback.Application.Services.TicketCategory;

public class TicketCategoryService : ITicketCategoryService
{
    private readonly ITicketCategoryRepository _ticketCategoryRepository;

    public TicketCategoryService(ITicketCategoryRepository ticketCategoryRepository)
    {
        _ticketCategoryRepository = ticketCategoryRepository;
    }

    public async Task<BaseResponse<bool>> InsertTicketCategoryAsync(TicketCategoryInsertRequest request)
    {
        var response = new BaseResponse<bool>();

        response.Data = await _ticketCategoryRepository.PostCategoryTicketAsync(new Domain.Entities.TicketCategory()
        {
            Description = request.Description,

        });

        return response;
    }

    public async Task<BaseResponse<List<TicketCategoryResponse>>> GetTicketCategoryAsync(TicketCategoryQuery query)
    {
        var response = new BaseResponse<List<TicketCategoryResponse>>();

        var queryable = (await _ticketCategoryRepository.GetTicketCategoryAsync(query));
        var data = queryable.Pagination<TicketCategoryResponse>(new BaseGetRequest()
        {
            Page = query.Page,
            PageSize = query.PageSize,
            SearchInput = query.SearchInput
        });

        if (query.IsDescending is null)
            response.Data = data.ToList().OrderBy(x => x.CreatedAt).ToList();
     
        if(query.IsDescending is not null)
        {
            if (query.IsDescending.Value)
                response.Data = data.ToList().OrderByDescending(x => x.TicketQuantity).ToList();

            if (!query.IsDescending.Value)
            {
                data.ToList().Sort((a, b) => a.TicketQuantity.CompareTo(b.TicketQuantity));
                response.Data = data.ToList();
            }
        }
        var totalRecord = queryable.Count();

        response.TotalRecords = totalRecord;
        response.PageSize = data.Count();
        response.Page = query.Page;
        return response;
    }
    public async Task<BaseResponse<List<OptionResponse>>> GetCategoryAsync()
    {
        var response = new BaseResponse<List<OptionResponse>>();

        response.Data = await _ticketCategoryRepository.GetCategoryAsync();

        return response; 
    }

    public async Task<BaseResponse<List<CategoryResponse>>> GetCategoryListAsync(BaseGetRequest query)
    {
        var response = new BaseResponse<List<CategoryResponse>>();
        var queryable = (await _ticketCategoryRepository.GetCategoryListAsync());
        var data = queryable.Pagination<CategoryResponse>(new BaseGetRequest()
        {
            Page = query.Page,
            PageSize = query.PageSize,
            SearchInput = query.SearchInput
        });

        response.TotalRecords = queryable.Count();
        response.PageSize = data.Count();
        response.Page = query.Page;
        response.Data = data.ToList();
        return response;
    }

    public async Task<BaseResponse<bool>> DeleteAsync(long entityId)
    {
        var response = new BaseResponse<bool>();
        var entity = await _ticketCategoryRepository.GetByIdAsync(entityId);
        response.Data = await _ticketCategoryRepository.DeleteAsync(entity, default);

        return response;
    }
}
