using Application.DTO.Album;
using Application.Exceptions;
using Application.UseCases.Queries.Albums;
using AutoMapper;
using DataAccess;
using Domain.Entities;
using Implementation.UseCases.Generics;
using Microsoft.EntityFrameworkCore;

namespace Implementation.UseCases.Queries;

public class EFFindAlbumQuery : GenericFind<Album, DetailedAlbumDto>, IFindAlbumQuery
{
    public EFFindAlbumQuery(TuneTraxContext context, IMapper mapper) : base(context, mapper)
    {
    }


    public override string Name => "Find Album";

    public override int Id => 33;

}
