using Application.DTO;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserValidator(TuneTraxContext context)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Wrong email format").Must(e => !context.Users.Any(user => user.Email == e)).WithMessage("Email is already used");

            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required").Matches("^[A-z0-9-_]{6,45}$").WithMessage("Wrong username format (Minimum length is 6, and it can contains letters, numbers and \"_\", \"-\")").Must(u => !context.Users.Any(user => user.Username == u)).WithMessage("Username is already used");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required").Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,}$").WithMessage("Wrong password format ( Minimal length is 8, must contain one uppercase, lowercase letter and number )");

            RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Date of birth is required").Must(d => DateTime.UtcNow.Year - d.Year >= 10).WithMessage("You must be older than 10 years");

            RuleFor(x => x.CountryId).NotEmpty().WithMessage("Country id is required").Must(t => context.Countries.Any(c => c.Id == t)).WithMessage("Country does not exists");
            RuleFor(x => x.Gender).Must(g => Enum.IsDefined(typeof(Gender), g)).WithMessage("Gender have not valid value ( only 0 (m) or 1 (f) ) ");
        }
    }
}
