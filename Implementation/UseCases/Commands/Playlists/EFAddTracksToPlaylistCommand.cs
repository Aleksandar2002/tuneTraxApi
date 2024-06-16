using Application.DTO.Playlist;
using Application.Exceptions;
using Application.UseCases.Commands.Playlists;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validators.Playlist;
using Microsoft.EntityFrameworkCore;

namespace Implementation.UseCases.Commands.Playlists;

public class EFAddTrackToPlaylistCommand : EFUseCase, IAddTracksToPlaylistCommand
{
    private ChangeTracksInPlaylistValidator _validator;
    public EFAddTrackToPlaylistCommand(TuneTraxContext context, ChangeTracksInPlaylistValidator validator) : base(context)
    {
        _validator = validator;

    }

    public string Name => "Add track to playlist";

    public int Id => 21;

    public void Execute(PlaylistTracksDto request)
    {
        _validator.ValidateAndThrow(request);

        var playlist = Context.Playlists.Where(x => x.Id == request.PlaylistId && x.IsActive).Include(x => x.Tracks).FirstOrDefault();

        // var trackIds = playlist.Tracks.Select(x => x.Id).Union(request.Tracks);

        // var trackIds = request.Tracks.Except(playlist.Tracks.Select(x => x.Id));

        // var tracks = Context.Tracks.Where(x => trackIds.Contains(x.Id)).ToList();

        // // if (playlist.Tracks.Any(x => x.Id == track.Id))
        // // {
        // //     throw new ValidationException("Track already exists in that playlist");
        // // }

        // playlist.Tracks.ToList().AddRange(tracks);


        if (playlist == null)
        {
            throw new EntityNotFoundException(request.PlaylistId.Value, nameof(Playlist));
        }


        var trackIds = request.Tracks.Except(playlist.Tracks.Select(x => x.Id));

        Context.PlaylistTrack.AddRange(trackIds.Select(x => new PlaylistTrack
        {
            TrackId = x,
            PlaylistId = playlist.Id
        }));

        Context.SaveChanges();
    }
}
