using System.ComponentModel.DataAnnotations;

namespace BHD.Domain.Entities;

public class Phone
{
    public Guid Id { get; set; }

    [Required]
    public string Number { get; set; }

    [Required]
    public string CityCode { get; set; }

    [Required]
    public string CountryCode { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }
}
