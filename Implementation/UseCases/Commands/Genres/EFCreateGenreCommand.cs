using Application.DTO.Genre;
using Application.UseCases.Commands.Genres;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validators.Genre;

namespace Implementation.UseCases.Commands.Genres;

public class EFCreateGenreCommand : EFUseCase, ICreateGenreCommand
{
    private CreateGenreValidator _validator;
    public EFCreateGenreCommand(TuneTraxContext context, CreateGenreValidator validator) : base(context)
    {
        _validator = validator;
    }

    public string Name => "Create genre";

    public int Id => 10;

    public void Execute(CreateGenreDto request)
    {
        _validator.ValidateAndThrow(request);

        Context.Genres.Add(new Genre
        {
            Name = request.Name
        });
        Context.SaveChanges();
    }
}
