using Application.DTO.Genre;
using DataAccess;
using FluentValidation;

namespace Implementation.Validators.Genre;

public class CreateGenreValidator : GenreCommandsValidator
{
    public CreateGenreValidator(TuneTraxContext context)
    {
        RuleFor(x => x.Name).Must(x => !context.Genres.Any(g => g.Name.ToLower() == x.ToLower())).WithMessage("Genre already exists");
    }
}
