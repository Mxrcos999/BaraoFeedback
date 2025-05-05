using BaraoFeedback.Application.DTOs.Category;
using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Domain.Entities;
using BaraoFeedback.Infra.Querys;

namespace BaraoFeedback.Application.Interfaces;

public interface ITicketCategoryRepository : IGenericRepository<TicketCategory>
{
    Task<IQueryable<CategoryResponse>> GetCategoryListAsync();
    Task<List<OptionResponse>> GetCategoryAsync();
    Task<bool> PostCategoryTicketAsync(Domain.Entities.TicketCategory entity);
    Task<IQueryable<TicketCategoryResponse>> GetTicketCategoryAsync(TicketCategoryQuery query);
}
