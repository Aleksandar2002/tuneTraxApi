using Application.Exceptions;
using Application.UseCases.Commands.Albums;
using DataAccess;
using Domain.Entities;

namespace Implementation.UseCases.Commands.Albums;

public class EFDeleteAlbumCommand : EFUseCase, IDeleteAlbumCommand
{
    public EFDeleteAlbumCommand(TuneTraxContext context) : base(context)
    {
    }


    public string Name => "Delete album";

    public int Id => 29;

    public void Execute(int request)
    {
        var album = Context.Albums.FirstOrDefault(x => x.Id == request && x.IsActive);

        if (album == null)
        {
            throw new EntityNotFoundException(request, nameof(Album));
        }

        if (album.Tracks.Any())
        {
            throw new ConflictException("Album cannot be deleted because it has some tracks");
        }

        album.IsActive = false;
        album.DeletedAt = DateTime.UtcNow;

        Context.SaveChanges();
    }

}
