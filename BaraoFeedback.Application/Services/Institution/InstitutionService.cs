using BaraoFeedback.Application.DTOs.Institution;
using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.DTOs.Ticket;
using BaraoFeedback.Application.Extensions;
using BaraoFeedback.Application.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BaraoFeedback.Application.Services.Institution;

public class InstitutionService : IInstitutionService
{
    private readonly IInstitutionRepository institutionRepository;

    public InstitutionService(IInstitutionRepository institutionRepository)
    {
        this.institutionRepository = institutionRepository;
    }
    
    public async Task<BaseResponse<bool>> PostInstitutionAsync(InstitutionInsertRequest request)
    {
        var response = new BaseResponse<bool>();
        var entity = new Domain.Entities.Institution(request.Name, request.Cep);

        response.Data = await institutionRepository.PostAsync(entity, default);

        return response;
    }    
    public async Task<BaseResponse<List<Domain.Entities.Institution>>> GetInstitutionAsync(BaseGetRequest request)
    {
        var response = new BaseResponse<List<Domain.Entities.Institution>>();
        var queryable = (await institutionRepository.GetAsync());
        var data = queryable.Pagination<Domain.Entities.Institution>(request);
        var totalRecord = queryable.Count();

        response.TotalRecords = totalRecord;
        response.PageSize = data.Count();
        response.Page = request.Page;
        response.Data = data.ToList();

        return response;
    }

    public async Task<List<OptionResponse>> GetInstitutionOptionsAsync()
    {
        return await institutionRepository.GetInstitutionOptionAsync();
    }

    public async Task<BaseResponse<bool>> DeleteAsync(long entityId)
    {
        var response = new BaseResponse<bool>();
        var entity = await institutionRepository.GetByIdAsync(entityId);
        response.Data = await institutionRepository.DeleteAsync(entity, default);

        return response;
    }
}
