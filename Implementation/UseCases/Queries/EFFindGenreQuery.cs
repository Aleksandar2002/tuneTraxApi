using Application.DTO.Genre;
using Application.UseCases.Queries.Genres;
using AutoMapper;
using DataAccess;
using Domain.Entities;
using Implementation.UseCases.Generics;

namespace Implementation.UseCases.Queries;

public class EFFindGenreQuery : GenericFind<Genre, GenreDto>, IFindGenreQuery
{
    public EFFindGenreQuery(TuneTraxContext context, IMapper mapper) : base(context, mapper)
    {
    }


    public override string Name => "Find genre";

    public override int Id => 35;

}
