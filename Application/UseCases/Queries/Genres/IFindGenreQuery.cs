using Application.DTO.Genre;

namespace Application.UseCases.Queries.Genres;

public interface IFindGenreQuery : IQuery<int, GenreDto>
{

}
