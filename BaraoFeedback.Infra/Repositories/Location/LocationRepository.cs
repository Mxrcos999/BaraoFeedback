using BaraoFeedback.Application.DTOs.Institution;
using BaraoFeedback.Application.DTOs.Location;
using BaraoFeedback.Application.Interfaces;
using BaraoFeedback.Infra.Context;
using BaraoFeedback.Infra.Repositories.Shared;
using System.Data.Entity;

namespace BaraoFeedback.Infra.Repositories.Location;

public class LocationRepository : GenericRepository<Domain.Entities.Location>, ILocationRepository
{
    private readonly BaraoFeedbackContext _context;
    public LocationRepository(BaraoFeedbackContext context) : base(context)
    {
        _context = context;
    }

    public async Task<LocationResponse> GetLocationWithAssociationAsync(long locationId)
    {
        var data = (from location in _context.Location
                   .AsNoTracking()
                   .Where(x => x.Id == locationId)
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
                    }).FirstOrDefault();

        return data;
    }
}
