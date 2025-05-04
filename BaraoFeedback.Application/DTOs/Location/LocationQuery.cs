using BaraoFeedback.Application.DTOs.Shared;

namespace BaraoFeedback.Application.DTOs.Location;

public class LocationQuery : BaseGetRequest
{
    public long InstitutionId { get; set; }
}
