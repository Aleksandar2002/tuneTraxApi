using Application;
using Application.DTO.Playlist;
using Application.UseCases.Queries.Playlists;
using AutoMapper;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Implementation.UseCases.Queries;

public class EFGetUserPlaylists : EFUseCase, IGetUserPlaylists
{
    private IApplicationActor _actor;
    private IMapper _mapper;
    public EFGetUserPlaylists(TuneTraxContext context, IApplicationActor actor, IMapper mapper) : base(context)
    {
        _actor = actor;
        _mapper = mapper;
    }

    public string Name => "Get user playlists";

    public int Id => 17;

    public PagedResponse<PlaylistDto> Execute(PlaylistSearch search)
    {
        var query = Context.Playlists.Where(x => x.UserId == _actor.Id && x.IsActive).AsQueryable();

        var totalCount = query.Count();
        var perPage = search.PerPage.HasValue ? search.PerPage.Value : 10;
        var page = search.Page.HasValue ? search.Page.Value : 10;

        query = query.Include(x => x.Tracks).Skip((page - 1) * perPage).Take(perPage);

        return new PagedResponse<PlaylistDto>
        {
            Data = query.Select(x => _mapper.Map<PlaylistDto>(x)).ToList(),
            TotalCount = totalCount,
            PerPage = perPage,
            CurrentPage = page
        };
    }
}
