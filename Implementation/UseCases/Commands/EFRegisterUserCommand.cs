using Application.DTO;
using Application.UseCases.Commands;
using BCrypt.Net;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands
{
    public class EFRegisterUserCommand : EFUseCase, IRegisterUserCommand
    {
        public RegisterUserValidator _validator;
        // public string _passwordSalt;
        public EFRegisterUserCommand(TuneTraxContext context, RegisterUserValidator validator) : base(context)
        {
            _validator = validator;
        }


        public string Name => "Registration";
        public int Id => 1;

        public void Execute(RegisterUserDto request)
        {
            _validator.ValidateAndThrow(request);

            string hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                CountryId = request.CountryId,
                DateOfBirth = request.DateOfBirth,
                Password = hashPassword,
                Gender = request.Gender,
                UseCases = UseCaseInfo.RegisteredUserAllowedUseCases
            };

            Context.Users.Add(user);
            Context.SaveChanges();

        }
    }
}
