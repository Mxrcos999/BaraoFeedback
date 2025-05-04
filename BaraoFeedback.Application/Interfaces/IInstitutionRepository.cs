using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Domain.Entities;

namespace BaraoFeedback.Application.Interfaces;

public interface IInstitutionRepository : IGenericRepository<Institution>
{
    Task<List<OptionResponse>> GetInstitutionOptionAsync();
}
