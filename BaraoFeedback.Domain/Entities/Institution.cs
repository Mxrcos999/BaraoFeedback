using BaraoFeedback.Domain.Entities.Base;

namespace BaraoFeedback.Domain.Entities;

public sealed class Institution : Entity
{
    public string Name { get; set; }
    public string Cep { get; set; }
}
