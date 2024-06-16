using Application.DTO.Playlist;
using Application.Exceptions;
using Application.UseCases.Commands.Playlists;
using DataAccess;
using Domain.Entities;
using FluentValidation;
using Implementation.Validators.Playlist;

namespace Implementation.UseCases.Commands.Playlists;

public class EFUpdatePlaylistCommand : EFUseCase, IUpdatePlaylistCommand
{
    private UpdatePlaylistValidator _validator;
    public EFUpdatePlaylistCommand(TuneTraxContext context, UpdatePlaylistValidator validator) : base(context)
    {
        _validator = validator;

    }

    public string Name => "Update playlist";

    public int Id => 19;

    public void Execute(UpdatePlaylistDto request)
    {
        _validator.ValidateAndThrow(request);

        var playlist = Context.Playlists.FirstOrDefault(x => x.Id == request.Id && x.IsActive);

        if (playlist == null)
        {
            throw new EntityNotFoundException(request.Id.Value, nameof(Playlist));
        }

        playlist.Name = request.Name;
        playlist.AccessLevel = request.AccessLevel;

        Context.SaveChanges();
    }
}
