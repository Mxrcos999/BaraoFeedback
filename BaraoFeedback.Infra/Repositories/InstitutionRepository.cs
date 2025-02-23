using BaraoFeedback.Application.Interfaces;
using BaraoFeedback.Domain.Entities;
using BaraoFeedback.Infra.Context;
using BaraoFeedback.Infra.Repositories.Shared;

namespace BaraoFeedback.Infra.Repositories;

public class InstitutionRepository : GenericRepository<Institution>, IInstitutionRepository
{
    public InstitutionRepository(BaraoFeedbackContext context) : base(context)
    {
    }
}
