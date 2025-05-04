using BaraoFeedback.Application.DTOs.Location;
using BaraoFeedback.Application.DTOs.Shared;

namespace BaraoFeedback.Application.Services.Location;

public interface ILocationService
{
    Task<List<OptionResponse>> GetLocationOptionsAsync();
    Task<BaseResponse<LocationResponse>> GetLocationByIdAsync(long locationId);
    Task<BaseResponse<bool>> PostLocationAsync(LocationInsertRequest request);
    Task<BaseResponse<bool>> DeleteAsync(long entityId);
    Task<BaseResponse<List<LocationResponse>>> GetLocationAsync(LocationQuery query);
}
