using BaraoFeedback.Application.DTOs.Category;
using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.Interfaces;
using BaraoFeedback.Infra.Context;
using BaraoFeedback.Infra.Querys;
using BaraoFeedback.Infra.Repositories.Shared;
using System.Data.Entity;

namespace BaraoFeedback.Infra.Repositories.TicketCategory;

public class TicketCategoryRepository : GenericRepository<Domain.Entities.TicketCategory>, ITicketCategoryRepository
{
    private readonly BaraoFeedbackContext _context;

    public TicketCategoryRepository(BaraoFeedbackContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IQueryable<TicketCategoryResponse>> GetTicketCategoryAsync(TicketCategoryQuery query)
    {
        var categoriesTickets = (from data in _context.TicketCategory
                      .AsNoTracking()
                      .Where(query.CreateFilterExpression())
                                 select new TicketCategoryResponse()
                                 {
                                     Description = data.Description,
                                     DescriptionId = data.Id,
                                     TicketQuantity = data.Tickets.Count()
                                 });

        return categoriesTickets;
    }
    public async Task<List<OptionResponse>> GetCategoryAsync()
    {
        var categoriesTickets = (from data in _context.TicketCategory
                      .AsNoTracking()
                                 select new OptionResponse()
                                 {
                                     Description = data.Description,
                                     Value = data.Id,
                                 }).ToList();

        return categoriesTickets;
    }

    public async Task<List<CategoryResponse>> GetCategoryListAsync()
    {
        var categoriesTickets = (from data in _context.TicketCategory
                      .AsNoTracking()
                                 select new CategoryResponse()
                                 {
                                     Description = data.Description,
                                     CategoryId = data.Id,
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
