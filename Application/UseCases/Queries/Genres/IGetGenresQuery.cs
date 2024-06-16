using Application.DTO.Genre;
using Application.UseCases;

namespace Application.UseCases.Queries.Genres;

public interface IGetGenresQuery : IQuery<GenreSearch, PagedResponse<GenreDto>>
{

}
