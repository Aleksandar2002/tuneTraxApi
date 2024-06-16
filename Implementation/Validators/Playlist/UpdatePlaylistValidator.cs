using Application;
using Application.DTO.Playlist;
using DataAccess;
using FluentValidation;

namespace Implementation.Validators.Playlist;

public class UpdatePlaylistValidator : PlaylistCommandsValidator<UpdatePlaylistDto>
{
    public UpdatePlaylistValidator(TuneTraxContext context, IApplicationActor actor)
    {
        RuleFor(x => x.Name).Must((dto, name) => !context.Playlists.Any(p => p.Id != dto.Id && p.Name.ToLower() == name.ToLower() && p.UserId == actor.Id)).WithMessage("You already have playlist with that name");
    }

}
