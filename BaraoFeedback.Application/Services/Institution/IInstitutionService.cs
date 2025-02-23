using BaraoFeedback.Application.DTOs.Institution;
using BaraoFeedback.Application.DTOs.Shared;

namespace BaraoFeedback.Application.Services.Institution;

public interface IInstitutionService
{
    Task<DefaultResponse> DeleteAsync(long entityId);
    Task<DefaultResponse> GetInstitutionAsync();
    Task<DefaultResponse> PostInstitutionAsync(InstitutionInsertRequest request);
}
