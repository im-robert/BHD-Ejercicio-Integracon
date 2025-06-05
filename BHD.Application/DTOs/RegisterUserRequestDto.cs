using System.ComponentModel.DataAnnotations;

namespace BHD.Application.DTOs;

public class RegisterUserRequestDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public List<PhoneDto> Phones { get; set; }
}
