using BHD.Application.DTOs;
using BHD.Application.Validators;
using BHD.Domain.Repositories;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Configuration;
using Moq;

namespace BHD.Tests.Validators;

public class RegisterUserValidatorTests
{
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly IConfiguration _config;
    private readonly RegisterUserValidator _validator;

    public RegisterUserValidatorTests()
    {
        _userRepoMock = new Mock<IUserRepository>();
        var inMemorySettings = new Dictionary<string, string> {
                { "Validation:EmailRegex", "^[\\w-]+(\\.[\\w-]+)*@([\\w-]+\\.)+[a-zA-Z]{2,7}$" },
                { "Validation:PasswordRegex", "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$" }
            };
        _config = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings).Build();
        _validator = new RegisterUserValidator(_config, _userRepoMock.Object);
    }

    [Fact]
    public async void Should_Have_Error_When_Email_Exists()
    {
        _userRepoMock.Setup(x => x.EmailExistsAsync("test@example.com")).ReturnsAsync(true);

        var dto = new RegisterUserRequestDto
        {
            Name = "Test",
            Email = "test@example.com",
            Password = "Password1",
            Phones = new List<PhoneDto>
                {
                    new PhoneDto { Number = "123", CityCode = "1", CountryCode = "57" }
                }
        };

        var result = await _validator.TestValidateAsync(dto);
        result.ShouldHaveValidationErrorFor(x => x.Email)
              .WithErrorMessage("El correo ya registrado");
    }
}
