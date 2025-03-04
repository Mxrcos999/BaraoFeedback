using BaraoFeedback.Domain.Entities.Base;

namespace BaraoFeedback.Domain.Entities;

public class Location : Entity
{

    public Location(string name, string description, long institutionId)
    {
        Name = name;
        Description = description;
        InstitutionId = institutionId;
    }
    public Location() { }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public Institution Institution { get; set; }
    public long InstitutionId { get; set; }
}
