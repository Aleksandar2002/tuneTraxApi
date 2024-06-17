using Application;
using Application.DTO.Playlist;
using DataAccess;
using FluentValidation;

namespace Implementation.Validators.Playlist;

public class ChangeTracksInPlaylistValidator : AbstractValidator<PlaylistTracksDto>
{
    public ChangeTracksInPlaylistValidator(TuneTraxContext context, IApplicationActor actor)
    {
        CascadeMode = CascadeMode.StopOnFirstFailure;
        RuleFor(x => x.PlaylistId).Must(x => context.Playlists.Any(p => p.Id == x && actor.Id == p.UserId)).WithMessage("Playlist does not exist");

        RuleFor(x => x.Tracks).Must(x => x != null && x.Count > 0).WithMessage("Tracks are required")
                    .Must(x =>
                    {
                        var existingTracks = context.Tracks.Where(tr => x.Contains(tr.Id) && tr.IsActive).Select(a => a.Id).ToList();
                        return existingTracks.Count == x.Count;
                    }).WithMessage("Some of the tracks does not exist in database");
    }

}
