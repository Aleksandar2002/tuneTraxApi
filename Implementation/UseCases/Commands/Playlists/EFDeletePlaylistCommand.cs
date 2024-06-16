using Application.Exceptions;
using Application.UseCases.Commands.Playlists;
using DataAccess;
using Domain.Entities;

namespace Implementation.UseCases.Commands.Playlists;

public class EFDeletePlaylistCommand : EFUseCase, IDeletePlaylistCommand
{
    public EFDeletePlaylistCommand(TuneTraxContext context) : base(context)
    {
    }

    public string Name => "Delete playlist";

    public int Id => 20;

    public void Execute(int request)
    {
        var playlist = Context.Playlists.Find(request);
        if (playlist == null || !playlist.IsActive || playlist.DeletedAt.HasValue)
        {
            throw new EntityNotFoundException(request, typeof(Playlist).Name);
        }

        var existingTracks = Context.PlaylistTrack.Where(x => x.PlaylistId == playlist.Id).ToList();
        Context.PlaylistTrack.RemoveRange(existingTracks);

        playlist.DeletedAt = DateTime.UtcNow;
        playlist.IsActive = false;

        Context.SaveChanges();
    }
}
