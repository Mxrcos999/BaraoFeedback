namespace BaraoFeedback.Application.DTOs.Location;

public class LocationInsertRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public long InstitutionId{ get; set; }
}