using Application.DTO.Album;

namespace Application.UseCases.Queries.Albums;

public interface IFindAlbumQuery : IQuery<int, DetailedAlbumDto>
{

}
