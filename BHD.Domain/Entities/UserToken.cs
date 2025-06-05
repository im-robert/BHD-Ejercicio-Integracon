using System.ComponentModel.DataAnnotations;

namespace BHD.Domain.Entities;

public class UserToken
{
    public Guid Id { get; set; }

    [Required]
    public string Token { get; set; }

    public DateTime IssuedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }
}
