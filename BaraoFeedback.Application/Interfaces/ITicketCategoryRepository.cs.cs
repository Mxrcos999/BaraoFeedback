using BaraoFeedback.Application.DTOs.Category;
using BaraoFeedback.Infra.Querys;

namespace BaraoFeedback.Application.Interfaces;

public interface ITicketCategoryRepository
{
    Task<bool> PostCategoryTicketAsync(Domain.Entities.TicketCategory entity);
    Task<List<CategoryResponse>> GetTicketAsync(TicketCategoryQuery query);
}
