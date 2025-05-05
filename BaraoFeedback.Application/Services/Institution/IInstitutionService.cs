using BaraoFeedback.Application.DTOs.Institution;
using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Domain.Entities;

namespace BaraoFeedback.Application.Services.Institution;

public interface IInstitutionService
{
    Task<BaseResponse<List<OptionResponse>>> GetInstitutionOptionsAsync();
    Task<BaseResponse<bool>> DeleteAsync(long entityId);
    Task<BaseResponse<List<Domain.Entities.Institution>>> GetInstitutionAsync(BaseGetRequest request);
    Task<BaseResponse<bool>> PostInstitutionAsync(InstitutionInsertRequest request);
}
