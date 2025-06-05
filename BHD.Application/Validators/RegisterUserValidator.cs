using BHD.Application.DTOs;
using BHD.Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace BHD.Application.Validators;

public class RegisterUserValidator : AbstractValidator<RegisterUserRequestDto>
{
    public RegisterUserValidator(IConfiguration config, IUserRepository userRepository)
    {
        // Validando email con regex desde configuración
        var emailPattern = config["Validation:EmailRegex"];
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es requerido.")
            .Matches(new Regex(emailPattern)).WithMessage("Formato de email invalido.")
            .MustAsync(async (email, ct) => !await userRepository.EmailExistsAsync(email))
            .WithMessage("El correo ya se encuentra registrado");

        // Validando password con regex desde configuración
        var passwordPattern = config["Validation:PasswordRegex"];
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es requerida.")
            .Matches(new Regex(passwordPattern)).WithMessage("La contraseña no cumple con los requerimientos.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es requerido. ");

        RuleForEach(x => x.Phones).ChildRules(phones =>
        {
            phones.RuleFor(p => p.Number).NotEmpty();
            phones.RuleFor(p => p.CityCode).NotEmpty();
            phones.RuleFor(p => p.CountryCode).NotEmpty();
        });
    }
}
