using Application;
using Application.DTO.Playlist;
using Application.Exceptions;
using Application.UseCases.Queries.Playlists;
using AutoMapper;
using DataAccess;
using Domain.Entities;
using Implementation.UseCases.Generics;

namespace Implementation.UseCases.Queries;

public class EFFindPlaylistQuery : EFUseCase, IFindPlaylistQuery
{
    private IMapper _mapper;
    private IApplicationActor _actor;
    public EFFindPlaylistQuery(TuneTraxContext context, IMapper mapper, IApplicationActor actor) : base(context)
    {
        _mapper = mapper;
        _actor = actor;
    }


    public string Name => "Find playlist";

    public int Id => 36;

    public PlaylistDto Execute(int search)
    {
        var playlist = Context.Playlists.FirstOrDefault(x => x.UserId == _actor.Id && x.Id == search && x.IsActive);

        if (playlist == null)
        {
            throw new EntityNotFoundException(search, nameof(Playlist));
        }

        return _mapper.Map<PlaylistDto>(playlist);
    }
}
