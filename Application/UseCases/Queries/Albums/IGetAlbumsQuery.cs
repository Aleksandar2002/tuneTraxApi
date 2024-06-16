using Application.DTO.Album;
using Application.UseCases.Queries.Searches;

namespace Application.UseCases.Queries.Albums;

public interface IGetAlbumsQuery : IQuery<AlbumSearch, PagedResponse<DetailedAlbumDto>>
{

}
