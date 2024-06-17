using Application.DTO.Album;
using Application.Exceptions;
using Application.UseCases.Commands.Albums;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validators.Albums;

namespace Implementation.UseCases.Commands.Albums;

public class EFUpdateAlbumCommand : EFUseCase, IUpdateAlbumCommand
{
    private CreateUpdateAlbumValidator _validator;
    public EFUpdateAlbumCommand(TuneTraxContext context, CreateUpdateAlbumValidator validator) : base(context)
    {
        _validator = validator;

    }


    public string Name => "Update album";

    public int Id => 30;

    public void Execute(UpdateAlbumDto request)
    {
        _validator.ValidateAndThrow(request);

        var album = Context.Albums.FirstOrDefault(x => x.Id == request.Id && x.IsActive);

        if (album == null)
        {
            throw new EntityNotFoundException(request.Id.Value, typeof(Album).Name);
        }

        album.ReleaseDate = request.ReleaseDate;
        album.ArtistId = request.ArtistId;
        album.Name = request.Name;

        Context.SaveChanges();


    }

}
