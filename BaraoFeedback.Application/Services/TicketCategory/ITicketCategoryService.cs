using BaraoFeedback.Application.DTOs.Category;
using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.Interfaces;
using BaraoFeedback.Infra.Querys;

namespace BaraoFeedback.Application.Services.TicketCategory;

public interface ITicketCategoryService
{ 
    Task<DefaultResponse> DeleteAsync(long entityId);
    Task<DefaultResponse> GetCategoryAsync();
    Task<DefaultResponse> GetTicketCategoryAsync(TicketCategoryQuery query);
    Task<DefaultResponse> InsertTicketCategoryAsync(TicketCategoryInsertRequest request);
}
