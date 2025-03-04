using BaraoFeedback.Application.DTOs.Location;
using BaraoFeedback.Application.DTOs.Shared;

namespace BaraoFeedback.Application.Services.Location;

public interface ILocationService
{
    Task<DefaultResponse> GetLocationByIdAsync(long locationId);
    Task<DefaultResponse> PostLocationAsync(LocationInsertRequest request);
    Task<DefaultResponse> DeleteAsync(long entityId);
    Task<DefaultResponse> GetLocationAsync(long institutionId);
}
