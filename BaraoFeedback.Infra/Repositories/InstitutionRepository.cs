using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.Interfaces;
using BaraoFeedback.Domain.Entities;
using BaraoFeedback.Infra.Context;
using BaraoFeedback.Infra.Repositories.Shared;
using System.Data.Entity;

namespace BaraoFeedback.Infra.Repositories;

public class InstitutionRepository : GenericRepository<Institution>, IInstitutionRepository
{
    public InstitutionRepository(BaraoFeedbackContext context) : base(context)
    {
    }

    public async Task<List<OptionResponse>> GetInstitutionOptionAsync()
    {
        var categories = (from data in _context.Institution
                      .AsNoTracking()
                                 select new OptionResponse()
                                 {
                                     Description = data.Name,
                                     Value = data.Id,
                                 }).ToList();

        return categories;
    }
}
