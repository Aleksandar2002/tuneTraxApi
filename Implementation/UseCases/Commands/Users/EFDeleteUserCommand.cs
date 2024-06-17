using Application.Exceptions;
using Application.UseCases.Commands.Users;
using DataAccess;
using Domain.Entities;

namespace Implementation.UseCases.Commands.Users;

public class EFDeleteUserCommand : EFUseCase, IDeleteUserCommand
{
    public EFDeleteUserCommand(TuneTraxContext context) : base(context)
    {
    }


    public string Name => "Delete user";

    public int Id => 38;

    public void Execute(int request)
    {
        var user = Context.Users.FirstOrDefault(x => x.Id == request && x.IsActive);

        if (user == null)
        {
            throw new EntityNotFoundException(request, nameof(User));
        }

        user.IsActive = false;
        user.DeletedAt = DateTime.UtcNow;

        Context.SaveChanges();
    }

}
