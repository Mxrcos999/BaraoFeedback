using BaraoFeedback.Domain.Entities.Base;

namespace BaraoFeedback.Domain.Entities;

public sealed class Institution : Entity
{
    public Institution(string name, string cep)
    {
        Name = name;
        Cep = cep;
    }
    public void Update(string name, string cep)
    {
        if (!string.IsNullOrEmpty(Name))
        {
            Name = name;
        }

        if (!string.IsNullOrEmpty(cep))
        {
            Cep = cep;
        }
    }

    public string Name { get; set; }
    public string Cep { get; set; }
    public IEnumerable<Location>? Locations { get; set; }
}
