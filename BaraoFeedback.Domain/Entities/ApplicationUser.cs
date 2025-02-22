using Microsoft.AspNetCore.Identity;

namespace BaraoFeedback.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string Type { get; set; }
    public string Name { get; set; }
}
