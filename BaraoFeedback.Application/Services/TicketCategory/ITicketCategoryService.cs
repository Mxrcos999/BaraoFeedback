using BaraoFeedback.Application.DTOs.Category;
using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.DTOs.Querys;

namespace BaraoFeedback.Application.Services.TicketCategory;

public interface ITicketCategoryService
{
    Task<BaseResponse<bool>> TicketCategoryUpdate(long entityId, TicketCategoryUpdateRequest model);
    Task<BaseResponse<List<CategoryResponse>>> GetCategoryListAsync(BaseGetRequest query);
    Task<BaseResponse<bool>> DeleteAsync(long entityId);
    Task<BaseResponse<List<OptionResponse>>> GetCategoryAsync();
    Task<BaseResponse<List<TicketCategoryResponse>>> GetTicketCategoryAsync(TicketCategoryQuery query);
    Task<BaseResponse<bool>> InsertTicketCategoryAsync(TicketCategoryInsertRequest request);
}
