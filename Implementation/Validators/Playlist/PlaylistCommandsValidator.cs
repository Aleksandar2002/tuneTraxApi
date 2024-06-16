using Application.DTO.Playlist;
using Domain.Entities;
using FluentValidation;

namespace Implementation.Validators.Playlist;

public class PlaylistCommandsValidator<T> : AbstractValidator<T> where T : CreatePlaylistDto
{
    public PlaylistCommandsValidator()
    {
        CascadeMode = CascadeMode.StopOnFirstFailure;
        RuleFor(x => x.Name).NotEmpty().WithMessage("Playlist name is required")
            .Matches("^[A-z0-9-_]{2,}$").WithMessage("Wrong playlist name format");


        RuleFor(x => x.AccessLevel).Must(a => Enum.IsDefined(typeof(AccessLevel), a)).WithMessage("Access level has not valid value ( only 0 (private) or 1 (public) ) ");
    }

}
