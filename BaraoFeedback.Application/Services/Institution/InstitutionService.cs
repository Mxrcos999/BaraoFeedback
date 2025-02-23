using BaraoFeedback.Application.DTOs.Institution;
using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.Interfaces;

namespace BaraoFeedback.Application.Services.Institution;

public class InstitutionService : IInstitutionService
{
    private readonly IInstitutionRepository institutionRepository;

    public InstitutionService(IInstitutionRepository institutionRepository)
    {
        this.institutionRepository = institutionRepository;
    }
    
    public async Task<DefaultResponse> PostInstitutionAsync(InstitutionInsertRequest request)
    {
        var response = new DefaultResponse();
        var entity = new Domain.Entities.Institution(request.Name, request.Cep);

        response.Data = await institutionRepository.PostAsync(entity, default);

        return response;
    }    
    public async Task<DefaultResponse> GetInstitutionAsync()
    {
        var response = new DefaultResponse(); 
        response.Data = await institutionRepository.GetAsync();

        return response;
    }
    public async Task<DefaultResponse> DeleteAsync(long entityId)
    {
        var response = new DefaultResponse();
        var entity = await institutionRepository.GetByIdAsync(entityId);
        response.Data = await institutionRepository.DeleteAsync(entity, default);

        return response;
    }
}
