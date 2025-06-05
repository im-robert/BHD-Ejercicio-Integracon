namespace BHD.Application.DTOs;

public class RegisterUserResponseDto
{
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public DateTime LastLogin { get; set; }
    public string Token { get; set; }
    public bool IsActive { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<PhoneDto> Phones { get; set; }
}
