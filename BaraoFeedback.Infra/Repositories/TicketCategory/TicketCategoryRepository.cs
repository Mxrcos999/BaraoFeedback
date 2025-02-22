using BaraoFeedback.Application.DTOs.Category;
using BaraoFeedback.Application.Interfaces;
using BaraoFeedback.Infra.Context;
using BaraoFeedback.Infra.Querys;
using System.Data.Entity;

namespace BaraoFeedback.Infra.Repositories.TicketCategory;

public class TicketCategoryRepository : ITicketCategoryRepository
{
    private readonly BaraoFeedbackContext _context;

    public TicketCategoryRepository(BaraoFeedbackContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryResponse>> GetTicketAsync(TicketCategoryQuery query)
    {
        var categoriesTickets = (from data in _context.TicketCategory
                      .AsNoTracking()
                      .Where(query.CreateFilterExpression())
                                 select new CategoryResponse()
                                 {
                                     Description = data.Description,
                                     DescriptionId = data.Id,
                                     TicketQuantity = data.Tickets.Count()
                                 }).ToList();

        return categoriesTickets;
    }

    public async Task<bool> PostCategoryTicketAsync(Domain.Entities.TicketCategory entity)
    {
        await _context.TicketCategory.AddAsync(entity);

        var result = await _context.SaveChangesAsync(default);

        if (result > 0)
            return true;

        return false;
    }
}
