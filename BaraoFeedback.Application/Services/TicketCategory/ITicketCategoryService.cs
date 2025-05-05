using BaraoFeedback.Application.DTOs.Category;
using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.Interfaces;
using BaraoFeedback.Infra.Querys;

namespace BaraoFeedback.Application.Services.TicketCategory;

public interface ITicketCategoryService
{
    Task<BaseResponse<List<CategoryResponse>>> GetCategoryListAsync();
    Task<BaseResponse<bool>> DeleteAsync(long entityId);
    Task<BaseResponse<List<OptionResponse>>> GetCategoryAsync();
    Task<BaseResponse<List<TicketCategoryResponse>>> GetTicketCategoryAsync(TicketCategoryQuery query);
    Task<BaseResponse<bool>> InsertTicketCategoryAsync(TicketCategoryInsertRequest request);
}
