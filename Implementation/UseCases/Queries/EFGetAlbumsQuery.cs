using Application;
using Application.DTO.Album;
using Application.UseCases.Queries.Albums;
using Application.UseCases.Queries.Searches;
using AutoMapper;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Implementation.UseCases.Queries;

public class EFGetAlbumsQuery : EFUseCase, IGetAlbumsQuery
{
    private IMapper _mapper;
    public EFGetAlbumsQuery(TuneTraxContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;

    }


    public string Name => "Get albums";

    public int Id => 27;

    public PagedResponse<DetailedAlbumDto> Execute(AlbumSearch search)
    {
        var albums = Context.Albums.Where(a => a.IsActive).Include(x => x.Artist).Include(x => x.Tracks).Select(x => _mapper.Map<DetailedAlbumDto>(x));

        var total = albums.Count();
        var page = search.Page.HasValue ? search.Page.Value : 1;
        var perPage = search.PerPage.HasValue ? search.PerPage.Value : 10;

        return new PagedResponse<DetailedAlbumDto>
        {
            Data = albums.Skip((page - 1) * perPage).Take(perPage),
            PerPage = perPage,
            CurrentPage = page,
            TotalCount = total
        };
    }

}
