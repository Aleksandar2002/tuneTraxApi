using Application.Exceptions;
using Application.UseCases.Commands.Artists;
using DataAccess;
using Domain.Entities;

namespace Implementation.UseCases.Commands.Artists;

public class EFDeleteArtistCommand : EFUseCase, IDeleteArtistCommand
{
    public EFDeleteArtistCommand(TuneTraxContext context) : base(context)
    {
    }

    public string Name => "Delete artist";

    public int Id => 16;

    public void Execute(int request)
    {
        var artist = Context.Artists.Find(request);
        if (artist == null || !artist.IsActive || artist.DeletedAt.HasValue)
        {
            throw new EntityNotFoundException(request, typeof(Artist).Name);
        }

        if (artist.Albums.Any())
        {
            throw new ConflictException("Artist cannot be deleted because he has some albums");
        }

        if (artist.Tracks.Any())
        {
            throw new ConflictException("Artist cannot be deleted because he has some tracks");
        }

        if (!string.IsNullOrEmpty(artist.Image))
        {
            string path = Path.Combine("wwwroot", "artists", artist.Image);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }


        var existingGenres = Context.ArtistGenre.Where(x => x.ArtistId == artist.Id).ToList();
        Context.ArtistGenre.RemoveRange(existingGenres);

        // FILE REMOVE

        artist.DeletedAt = DateTime.UtcNow;
        artist.IsActive = false;

        Context.SaveChanges();
    }
}
