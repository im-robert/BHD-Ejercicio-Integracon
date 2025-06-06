using Microsoft.AspNetCore.Identity;

namespace BHD.Domain.Entities;

public class User : IdentityUser<Guid>
{

    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public DateTime LastLogin { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<Phone> Phones { get; set; } = new List<Phone>();

    public ICollection<UserToken>? Tokens { get; set; } = new List<UserToken>();

}
