using BaraoFeedback.Application.DTOs.Institution;
using BaraoFeedback.Application.DTOs.Location;
using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.Extensions;
using BaraoFeedback.Application.Interfaces;

namespace BaraoFeedback.Application.Services.Location;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;

    public LocationService(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<BaseResponse<bool>> PostLocationAsync(LocationInsertRequest request)
    {
        var response = new BaseResponse<bool>();
        var entity = new Domain.Entities.Location(request.Name, request.Description, request.InstitutionId);

        response.Data = await _locationRepository.PostAsync(entity, default);

        return response;
    }

    public async Task<List<OptionResponse>> GetLocationOptionsAsync()
    {
        return await _locationRepository.GetLocationOptionAsync();
    }
    public async Task<BaseResponse<List<LocationResponse>>> GetLocationAsync(LocationQuery query)
    {
        var response = new BaseResponse<List<LocationResponse>>();


        var queryable = (from location in (await _locationRepository
                         .GetAsync(x => x.InstitutionId == query.InstitutionId))
                         select new LocationResponse()
                         {
                             Id = location.Id,
                             Description = location.Description,
                             Name = location.Name,
                             Institution = new IntitutionResponse()
                             {
                                 Name = location.Institution.Name,
                                 Id = location.Institution.Id
                             }
                         });

        var data = queryable.Pagination<LocationResponse>(new BaseGetRequest() { Page = query.Page, PageSize = query.PageSize, SearchInput = query.SearchInput });
        var totalRecord = queryable.Count();

        response.TotalRecords = totalRecord;
        response.PageSize = data.Count();
        response.Page = query.Page;
        response.Data = data.ToList();

        return response;
    }
    public async Task<BaseResponse<LocationResponse>> GetLocationByIdAsync(long locationId)
    {
        var response = new BaseResponse<LocationResponse>();

        response.Data = await _locationRepository.GetLocationWithAssociationAsync(locationId);

        return response;
    }
    public async Task<BaseResponse<bool>> DeleteAsync(long entityId)
    {
        var response = new BaseResponse<bool>();
        var entity = await _locationRepository.GetByIdAsync(entityId);
        response.Data = await _locationRepository.DeleteAsync(entity, default);

        return response;
    }
}
