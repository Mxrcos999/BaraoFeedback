using BaraoFeedback.Application.DTOs.Location;
using BaraoFeedback.Application.DTOs.Shared;
using BaraoFeedback.Domain.Entities;

namespace BaraoFeedback.Application.Interfaces;

public interface ILocationRepository : IGenericRepository<Location>
{
    Task<List<OptionResponse>> GetLocationOptionAsync();
    Task<LocationResponse> GetLocationWithAssociationAsync(long locationId);
}
