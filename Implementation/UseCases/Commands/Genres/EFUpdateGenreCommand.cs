using Application.DTO.Genre;
using Application.Exceptions;
using Application.UseCases.Commands.Genres;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validators.Genre;

namespace Implementation.UseCases.Commands.Genres;

public class EFUpdateGenreCommand : EFUseCase, IUpdateGenreCommand
{
    private UpdateGenreValidator _validator;
    public EFUpdateGenreCommand(TuneTraxContext context, UpdateGenreValidator validator) : base(context)
    {
        _validator = validator;
    }

    public string Name => "Update genre";

    public int Id => 12;

    public void Execute(UpdateGenreDto request)
    {
        _validator.ValidateAndThrow(request);

        var genre = Context.Genres.FirstOrDefault(x => x.Id == request.Id && x.IsActive);

        if (genre == null)
        {
            throw new EntityNotFoundException(request.Id.Value, nameof(Genre));
        }

        genre.Name = request.Name;

        Context.SaveChanges();
    }
}