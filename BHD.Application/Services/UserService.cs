using AutoMapper;
using BHD.Application.DTOs;
using BHD.Domain.Entities;
using BHD.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BHD.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public UserService(
        IUserRepository userRepository,
        UserManager<User> userManager,
        IMapper mapper,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllUserAsync();

        var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

        return usersDto;

    }

    public async Task<RegisterUserResponseDto> RegisterUserAsync(RegisterUserRequestDto request)
    {

        var user = new User
        {
            UserName = request.Email,
            Email = request.Email,
            Created = DateTime.UtcNow,
            Modified = DateTime.UtcNow,
            LastLogin = DateTime.UtcNow,
            IsActive = true,
        };


        foreach (var p in request.Phones)
        {
            user.Phones.Add(new Phone
            {
                Id = Guid.NewGuid(),
                Number = p.Number,
                CityCode = p.CityCode,
                CountryCode = p.CountryCode
            });
        }

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new Exception(string.Join("; ", result.Errors));
        }

        var tokenValue = GenerateJwtToken(user);

  
        var userToken = new UserToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = tokenValue,
            IssuedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddMinutes(
                    double.Parse(_configuration["JwtSettings:ExpiryMinutes"]))
        };
        await _userRepository.AddTokenAsync(userToken);

        await _userRepository.SaveChangesAsync();


        var response = new RegisterUserResponseDto
        {
            Id = user.Id,
            Created = user.Created,
            Modified = user.Modified,
            LastLogin = user.LastLogin,
            Token = tokenValue,
            IsActive = user.IsActive,
            Name = request.Name,
            Email = user.Email,
            Phones = request.Phones
        };

        return response;
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"]));

        var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
