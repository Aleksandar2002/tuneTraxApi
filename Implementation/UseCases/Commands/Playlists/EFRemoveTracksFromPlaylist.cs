using Application.DTO.Playlist;
using Application.Exceptions;
using Application.UseCases.Commands.Playlists;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validators.Playlist;
using Microsoft.EntityFrameworkCore;

namespace Implementation.UseCases.Commands.Playlists;

public class EFRemoveTracksFromPlaylist : EFUseCase, IRemoveTracksFromPlaylist
{
    private ChangeTracksInPlaylistValidator _validator;
    public EFRemoveTracksFromPlaylist(TuneTraxContext context, ChangeTracksInPlaylistValidator validator) : base(context)
    {
        _validator = validator;

    }

    public string Name => "Remove tracks from playlist";

    public int Id => 22;

    public void Execute(PlaylistTracksDto request)
    {
        _validator.ValidateAndThrow(request);

        var playlist = Context.Playlists.Where(x => x.Id == request.PlaylistId && x.IsActive).Include(x => x.Tracks).FirstOrDefault();

        if (playlist == null)
        {
            throw new EntityNotFoundException(request.PlaylistId.Value, nameof(Playlist));
        }

        if (request.Tracks.Any(x => !playlist.Tracks.Any(t => t.Id == x && t.IsActive)))
        {
            throw new ValidationException("Some track are not in the playlist");
        }

        var tracksInPlaylist = Context.PlaylistTrack.Where(x => x.PlaylistId == request.PlaylistId && request.Tracks.Contains(x.TrackId)).ToList();
        Context.PlaylistTrack.RemoveRange(tracksInPlaylist);

        Context.SaveChanges();
    }
}
