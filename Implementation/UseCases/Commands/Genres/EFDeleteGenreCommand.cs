using Application.Exceptions;
using Application.UseCases.Commands.Genres;
using DataAccess;
using Domain.Entities;
using Implementation.UseCases;

namespace Implementation.UseCases.Commands.Genres;

public class EFDeleteGenreCommand : EFUseCase, IDeleteGenreCommand
{
    public EFDeleteGenreCommand(TuneTraxContext context) : base(context)
    {
    }

    public string Name => "Delete genre";

    public int Id => 11;

    public void Execute(int request)
    {
        var genre = Context.Genres.Find(request);
        if (genre == null || !genre.IsActive || genre.DeletedAt.HasValue)
        {
            throw new EntityNotFoundException(request, typeof(Genre).Name);
        }

        if (genre.Artists.Any())
        {
            throw new ConflictException("Genre belongs to some artists, it cannot be deleted");
        }
        if (genre.Tracks.Any())
        {
            throw new ConflictException("Genre belongs to some tracks, it cannot be deleted");
        }

        genre.IsActive = false;
        genre.DeletedAt = DateTime.UtcNow;

        // Context.Genres.Remove(genre);
        Context.SaveChanges();

    }
}
