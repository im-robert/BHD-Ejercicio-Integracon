using BHD.Application.DTOs;

namespace BHD.Application.Services;

public interface IUserService
{
    Task<RegisterUserResponseDto> RegisterUserAsync(RegisterUserRequestDto request);
    Task<IEnumerable<UserDto>> GetAllAsync();
}
