using Application;
using Application.DTO.Genre;
using Application.UseCases.Queries.Genres;
using DataAccess;
using Implementation.UseCases;

namespace Implementation;

public class EFGetGenresQuery : EFUseCase, IGetGenresQuery
{
    public EFGetGenresQuery(TuneTraxContext context) : base(context)
    {
    }

    public string Name => "Get genres";

    public int Id => 9;
    public PagedResponse<GenreDto> Execute(GenreSearch search)
    {
        var genres = Context.Genres.Where(x => x.IsActive).Select(x => new GenreDto
        {
            Name = x.Name,
            Id = x.Id
        });

        var totalCount = genres.Count();
        var perPage = search.PerPage.HasValue ? search.PerPage.Value : 10;
        var page = search.Page.HasValue ? search.Page.Value : 1;

        return new PagedResponse<GenreDto>
        {
            PerPage = perPage,
            CurrentPage = page,
            TotalCount = totalCount,
            Data = genres.Skip((page - 1) * perPage).Take(perPage)
        };
    }
}
