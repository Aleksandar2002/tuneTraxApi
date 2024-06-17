using Application.DTO.Artist;

namespace Application.UseCases.Queries.Artists;

public interface IFindArtistQuery : IQuery<int, DetailedArtistDto>
{

}
