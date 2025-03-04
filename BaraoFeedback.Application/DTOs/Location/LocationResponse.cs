using BaraoFeedback.Application.DTOs.Institution;

namespace BaraoFeedback.Application.DTOs.Location;

public class LocationResponse
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IntitutionResponse Institution { get; set; }
}
