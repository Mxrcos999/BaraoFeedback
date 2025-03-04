using BaraoFeedback.Application.DTOs.Location;
using BaraoFeedback.Domain.Entities;

namespace BaraoFeedback.Application.Interfaces;

public interface ILocationRepository : IGenericRepository<Location>
{
    Task<LocationResponse> GetLocationWithAssociationAsync(long locationId);
}
