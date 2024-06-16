using System.Security.Cryptography.X509Certificates;
using Application;
using Application.DTO.Playlist;
using Application.UseCases.Commands.Playlists;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validators.Playlist;

namespace Implementation.UseCases.Commands.Playlists;

public class EFCreatePlaylistCommand : EFUseCase, ICreatePlaylistCommand
{
    private CreatePlaylistValidator _validator;
    private IApplicationActor _actor;
    public EFCreatePlaylistCommand(TuneTraxContext context, CreatePlaylistValidator validator, IApplicationActor actor) : base(context)
    {
        _validator = validator;
        _actor = actor;
    }

    public string Name => "Create playlist command";

    public int Id => 18;

    public void Execute(CreatePlaylistDto request)
    {
        _validator.ValidateAndThrow(request);

        Context.Playlists.Add(new Playlist
        {
            Name = request.Name,
            AccessLevel = request.AccessLevel,
            UserId = _actor.Id

        });
        Context.SaveChanges();
    }
}
