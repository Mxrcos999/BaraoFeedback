using BaraoFeedback.Application.DTOs.Category;
using BaraoFeedback.Domain.Entities;
using BaraoFeedback.Infra.Querys;

namespace BaraoFeedback.Application.Interfaces;

public interface ITicketCategoryRepository : IGenericRepository<TicketCategory>
{
    Task<List<CategoryResponse>> GetCategoryAsync();
    Task<bool> PostCategoryTicketAsync(Domain.Entities.TicketCategory entity);
    Task<List<TicketCategoryResponse>> GetTicketCategoryAsync(TicketCategoryQuery query);
}
