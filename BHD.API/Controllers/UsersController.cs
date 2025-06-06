using BHD.Application.DTOs;
using BHD.Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;

namespace BHD.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IValidator<RegisterUserRequestDto> _validator;

    public UsersController(IUserService userService, IValidator<RegisterUserRequestDto> validator)
    {
        _userService = userService;
        _validator = validator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequestDto request)
    {

        ValidationResult result = await _validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            return BadRequest(new { mensaje = result.Errors[0].ErrorMessage });
        }

        try
        {
            var response = await _userService.RegisterUserAsync(request);
            return Ok(response);
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);

    }
}
