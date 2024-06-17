using System.Data;
using Application.DTO.User;
using DataAccess;
using FluentValidation;

namespace Implementation.Validators.Users;

public class UpdateUserPrivilegesValidator : AbstractValidator<UpdateUserPrivilegesDto>
{
    private int updatePrivilegeUseCaseId = 3;
    public UpdateUserPrivilegesValidator(TuneTraxContext context)
    {
        CascadeMode = CascadeMode.StopOnFirstFailure;
        RuleFor(x => x.UserId).Must(x => context.Users.Any(u => u.Id == x)).WithMessage("User does not exist")
                            .Must(x => !context.UserUseCases.Any(u => u.UserId == x && u.UseCaseId == updatePrivilegeUseCaseId)).WithMessage("Cannot change use cases for this user");

        RuleFor(x => x.UseCaseIds).Must(x => !x.Any(id => id < 0 || id >= UseCaseInfo.MaxUseCaseId)).WithMessage("There is some use cases that do not exist")
                .Must(x => x.Distinct().Count() == x.Count()).WithMessage("Use cases must be unique");
    }

}
