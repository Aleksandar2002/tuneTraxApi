using Application.DTO.Artist;
using Application.UseCases.Queries.Artists;
using AutoMapper;
using DataAccess;
using Domain.Entities;
using Implementation.UseCases.Generics;

namespace Implementation.UseCases.Queries;

public class EFFindArtistQuery : GenericFind<Artist, DetailedArtistDto>, IFindArtistQuery
{
    public EFFindArtistQuery(TuneTraxContext context, IMapper mapper) : base(context, mapper)
    {
    }


    public override string Name => "Find artist";

    public override int Id => 31;


}
