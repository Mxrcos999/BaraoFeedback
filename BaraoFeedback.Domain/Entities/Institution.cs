using BaraoFeedback.Domain.Entities.Base;

namespace BaraoFeedback.Domain.Entities;

public sealed class Institution : Entity
{
    public Institution(string name, string cep)
    {
        Name = name;
        Cep = cep;
    }

    public string Name { get; set; }
    public string Cep { get; set; }
}
