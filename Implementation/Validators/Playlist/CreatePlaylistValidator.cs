using System.Data;
using Application;
using Application.DTO.Playlist;
using DataAccess;
using Domain.Entities;
using FluentValidation;

namespace Implementation.Validators.Playlist;

public class CreatePlaylistValidator : PlaylistCommandsValidator<CreatePlaylistDto>
{
    public CreatePlaylistValidator(TuneTraxContext context, IApplicationActor actor)
    {

        RuleFor(x => x.Name).Must(x => !context.Playlists.Any(p => p.Name.ToLower() == x.ToLower() && p.UserId == actor.Id)).WithMessage("You already have playlist with that name");
    }

}
