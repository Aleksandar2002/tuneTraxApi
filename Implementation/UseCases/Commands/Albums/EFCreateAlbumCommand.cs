using Application.DTO.Album;
using Application.UseCases.Commands.Albums;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validators.Albums;
using Microsoft.Identity.Client;

namespace Implementation.UseCases.Commands.Albums;

public class EFCreateAlbumCommand : EFUseCase, ICreateAlbumCommand
{
    public CreateUpdateAlbumValidator _validator;
    public EFCreateAlbumCommand(TuneTraxContext context, CreateUpdateAlbumValidator validator) : base(context)
    {
        _validator = validator;

    }


    public string Name => "Create album";

    public int Id => 28;

    public void Execute(CreateAlbumDto request)
    {
        _validator.ValidateAndThrow(request);

        Context.Albums.Add(new Album
        {
            Name = request.Name,
            ReleaseDate = request.ReleaseDate,
            ArtistId = request.ArtistId,
        });

        Context.SaveChanges();
    }

}
