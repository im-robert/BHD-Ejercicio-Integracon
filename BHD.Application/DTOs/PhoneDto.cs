using System.ComponentModel.DataAnnotations;

namespace BHD.Application.DTOs;

public class PhoneDto
{
    [Required]
    public string Number { get; set; }

    [Required]
    public string CityCode { get; set; }

    [Required]
    public string CountryCode { get; set; }
}
