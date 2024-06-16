using Application.DTO.User;
using Application.UseCases.Commands.Users;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validators.Users;

namespace Implementation.UseCases.Commands.Users;

public class EFUpdateUserPrivilegesCommand : EFUseCase, IUpdateUserPrivilegesCommand
{
    private UpdateUserPrivilegesValidator _validator;
    public EFUpdateUserPrivilegesCommand(TuneTraxContext context, UpdateUserPrivilegesValidator validator) : base(context)
    {
        _validator = validator;

    }


    public string Name => "Update user privileges";

    public int Id => 3;

    public void Execute(UpdateUserPrivilegesDto request)
    {
        _validator.ValidateAndThrow(request);

        var existingUseCases = Context.UserUseCases.Where(x => x.UserId == request.UserId).ToList();
        Context.UserUseCases.RemoveRange(existingUseCases);

        Context.UserUseCases.AddRange(request.UseCaseIds.Select(x => new UserUseCase
        {
            UseCaseId = x,
            UserId = request.UserId
        }));
        Context.SaveChanges();
    }
}
