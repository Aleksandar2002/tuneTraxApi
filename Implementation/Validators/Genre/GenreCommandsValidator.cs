using Application.DTO.Genre;
using FluentValidation;

namespace Implementation.Validators.Genre;

public class GenreCommandsValidator : AbstractValidator<CreateGenreDto>
{
    public GenreCommandsValidator()
    {
        CascadeMode = CascadeMode.StopOnFirstFailure;

        RuleFor(x => x.Name).NotEmpty().WithMessage("Genre name is required")
        .Matches("^[A-z0-9-_]{2,}$").WithMessage("Wrong genre name format");

    }

}
