using BaraoFeedback.Application.DTOs.Institution;
using BaraoFeedback.Application.DTOs.Location;
using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Application.Interfaces;
using BaraoFeedback.Domain.Entities;
using System.Data.Entity;

namespace BaraoFeedback.Application.Services.Location;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;

    public LocationService(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<DefaultResponse> PostLocationAsync(LocationInsertRequest request)
    {
        var response = new DefaultResponse();
        var entity = new Domain.Entities.Location(request.Name, request.Description, request.InstitutionId);

        response.Data = await _locationRepository.PostAsync(entity, default);

        return response;
    }
    public async Task<DefaultResponse> GetLocationAsync(long institutionId)
    {
        var response = new DefaultResponse();

        response.Data =  (from data in (await _locationRepository
                         .GetAsync(x => x.InstitutionId == institutionId))
                        select new LocationResponse()
                        {
                            Id = data.Id,
                            Description = data.Description,
                            Name = data.Name,
                            Institution = new IntitutionResponse()
                            {
                                Name = data.Institution.Name,
                                Id = data.Institution.Id
                            }
                        }).ToList();

        return response;
    }
    public async Task<DefaultResponse> GetLocationByIdAsync(long locationId)
    {
        var response = new DefaultResponse();
        response.Data = await _locationRepository.GetLocationWithAssociationAsync(locationId);

        return response;
    } 
    public async Task<DefaultResponse> DeleteAsync(long entityId)
    {
        var response = new DefaultResponse();
        var entity = await _locationRepository.GetByIdAsync(entityId);
        response.Data = await _locationRepository.DeleteAsync(entity, default);

        return response;
    }
}
